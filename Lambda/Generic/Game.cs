using Lambda.Events;
using Lambda.Models;
using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.Diagnostics;
using System.Net;

namespace Lambda.Generic {

    public enum ValidationResult {
        Indeterminable = -2,
        Invalid = -1,
        Unknown = 0,
        Valid = 1
    }

    public class FileValidation(FileInfo file, ValidationResult result) {
        public FileInfo File { get; } = file;
        public ValidationResult Result { get; } = result;
    }

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ValidationFile : IEquatable<ValidationFile> {
        public static ValidationFile Empty { get; } = new();
        public static ValidationFile Error { get; } = new() { Filename = "<ERR>", Hash = "<ERR>" };

        public FileInfo? File { get; }

        [JsonProperty("path", Required = Required.Always)]
        public string Filename { get; set; } = string.Empty;

        [JsonProperty("hash", Required = Required.Always)]
        public string Hash { get; set; } = string.Empty;

        public bool Equals(ValidationFile? obj)
            => obj != null && File == obj.File && Filename == obj.Filename && Hash == obj.Hash;

        public override bool Equals(object? obj)
            => Equals(obj as ValidationFile);

        public override int GetHashCode()
            => new int[File?.GetHashCode() ?? 0.GetHashCode(), Filename.GetHashCode(), Hash.GetHashCode()].GetHashCode();
    }

    public class Game : GameModel {
        public const string DEFAULT_VALIDATION_FILE = "_validate.lambda";

        public static event EventHandler<FileProgressChangedEventArgs>? OnCreateHashesProgressChanged;

        public event EventHandler<FileProgressChangedEventArgs>? OnValidationProgressChanged;

        #region Properties
        public IPEndPoint? RemoteLocation { get; }
        #endregion Properties

        #region Construct

        public Game(string identifier, string name, string location) {
            Identifier = identifier;
            Name = name;
            Location = location;

            if (!IsRemote && !checkLocation())
                throw new DirectoryNotFoundException("Directory '" + Location + "' could not be located!");
        }

        #endregion Construct

        #region Methods

        private static async Task createHashes(DirectoryInfo location) {
            FileInfo hashFile = new(Path.Combine(location.FullName, DEFAULT_VALIDATION_FILE));

            if (hashFile.Exists)
                hashFile.Delete();

            FileInfo[] files = location.GetFiles("*.*", SearchOption.AllDirectories);

            List<ValidationFile> hashFiles = [];
            int fileCount = files.Length + 1;
            int currentFileNum = 0;

            foreach (FileInfo f in files) {
                if (f.Name is not "." or ".." && f.FullName != hashFile.FullName) {
                    currentFileNum++;
                    OnCreateHashesProgressChanged?.Invoke(null, new FileProgressChangedEventArgs(f, currentFileNum, fileCount, false));

                    string relPath = Path.GetRelativePath(location.FullName, f.FullName);
                    hashFiles.Add(new ValidationFile() {
                        Filename = relPath,
                        Hash = await Hashing.ComputeSha512HashFromFile(f)
                    });

                    OnCreateHashesProgressChanged?.Invoke(null, new FileProgressChangedEventArgs(f, currentFileNum, fileCount, true));
                }
            }

            OnCreateHashesProgressChanged?.Invoke(null, new FileProgressChangedEventArgs(hashFile, fileCount, fileCount, false));
            await File.WriteAllTextAsync(hashFile.FullName, JsonConvert.SerializeObject(hashFiles, Formatting.Indented));
            OnCreateHashesProgressChanged?.Invoke(null, new FileProgressChangedEventArgs(hashFile, fileCount, fileCount, true));
        }

        private static async Task executeCommands(IEnumerable<CommandModel> commands, string workingDirectory, Action<Process> callback)
            => await commands.ForEachAsync(async cmd => {
                Process p = new() {
                    EnableRaisingEvents = true,
                    StartInfo = await cmd.ToProcessStartInfo(),
                };

                if (!workingDirectory.IsNull())
                    p.StartInfo.WorkingDirectory = workingDirectory;

                p.Exited += (sender, e) => callback?.Invoke(p);
                p.Start();

                if (cmd.WaitForExit)
                    await p.WaitForExitAsync();

                await Task.CompletedTask;
            });

        private async Task<List<FileValidation>> validate(FileInfo hashFile) {
            List<FileValidation> fileValidationList = [];
            DirectoryInfo dir = new(Path.GetFullPath(Location));

            string hashFileJson = await File.ReadAllTextAsync(hashFile.FullName);
            List<ValidationFile> validationFileList = JsonConvert.DeserializeObject<List<ValidationFile>>(hashFileJson) ?? [];
            FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

            await files.ForEachAsync(async f => {
                if (f.FullName != hashFile.FullName) {
                    string relPath = Path.GetRelativePath(dir.FullName, f.FullName);
                    ValidationFile val = validationFileList.FirstOrDefault(v => v.Filename == relPath, ValidationFile.Empty);
                    ValidationResult result = ValidationResult.Unknown;

                    if (!val.Equals(ValidationFile.Empty) && !val.Equals(ValidationFile.Error)) {
                        string computedHash = await Hashing.ComputeSha512HashFromFile(f);

                        result = !computedHash.IsNull() && computedHash == val.Hash
                            ? ValidationResult.Valid
                            : ValidationResult.Invalid;
                    } else
                        result = ValidationResult.Indeterminable;

                    fileValidationList.Add(new FileValidation(f, result));
                }

                await Task.CompletedTask;
            });

            return fileValidationList;
        }

        private bool checkLocation()
            => !Location.IsNull() && Directory.Exists(Path.GetFullPath(Location));

        public async Task Execute(Action<Process> callback) {
            string dir = !IsStandalone && !InstallLocation.IsNull() ? InstallLocation : Location;

            if (ExecuteCommands != null)
                await executeCommands(ExecuteCommands, dir, callback);

            if (ClosedCommands != null)
                await executeCommands(ClosedCommands, dir, callback);
        }

        public static async Task<Game> Import(string identifier, string name, string location) {
            DirectoryInfo dir = new(Path.GetFullPath(location));

            if (dir.Exists)
                await createHashes(dir);

            return new(identifier, name, location);
        }

        public async Task<bool> Fetch() {
            throw new NotImplementedException();
        }

        public async Task Install(Action<Process> callback) {
            if (!IsInstalled && !IsStandalone && InstallerCommands != null)
                await executeCommands(InstallerCommands, Location, callback);
        }

        public async Task Prepare(Action<Process> callback) {
            if (!IsPrepared && PrepareCommands != null)
                await executeCommands(PrepareCommands, Location, callback);
        }

        public async Task<List<FileValidation>> Validate() {
            if (IsRemote || (IsStandalone && IsPrepared))
                return [];

            FileInfo hashFile = new(Path.Combine(Path.GetFullPath(Location), DEFAULT_VALIDATION_FILE));
            return hashFile.Exists ? await validate(hashFile) : [];
        }

        public static Game FromModel(GameModel model)
            => new(model.Identifier, model.Name, model.Location) {
                Description = model.Description,
                ExecuteCommands = model.ExecuteCommands,
                FirstStartCommands = model.FirstStartCommands,
                Genres = model.Genres,
                ImagePath = model.ImagePath,
                InstallerCommands = model.InstallerCommands,
                IsExecuted = model.IsExecuted,
                IsPrepared = model.IsPrepared,
                IsPublic = model.IsPublic,
                IsRemote = model.IsRemote,
                IsStandalone = model.IsStandalone,
                PrepareCommands = model.PrepareCommands
            };

        #endregion Methods
    }
}
