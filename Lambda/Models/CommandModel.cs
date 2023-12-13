using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.ComponentModel;
using System.Diagnostics;

namespace Lambda.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CommandModel
    {
        [JsonProperty("arguments", Required = Required.Always)]
        public List<string> Arguments { get; set; } = [];

        [JsonProperty("createNoWindow", Required = Required.Always)]
        [DefaultValue(false)]
        public bool CreateNoWindow { get; set; }

        [JsonProperty("commandline", Required = Required.Always)]
        [DefaultValue("")]
        public string CommandLine { get; set; } = string.Empty;

        [JsonProperty("useShellExecute", Required = Required.Always)]
        [DefaultValue(false)]
        public bool UseShellExecute { get; set; }

        [JsonProperty("waitForExit", Required = Required.Always)]
        [DefaultValue(false)]
        public bool WaitForExit { get; set; }

        [JsonProperty("windowStyle", Required = Required.Always)]
        [DefaultValue(ProcessWindowStyle.Normal)]
        public ProcessWindowStyle WindowStyle { get; set; } = ProcessWindowStyle.Normal;

        public CommandModel(string commandline) : this(commandline, new List<string>()) { }
        public CommandModel(string commandline, IEnumerable<string> arguments)
        {
            if (commandline.IsNull())
                throw new ArgumentNullException(nameof(commandline));

            Arguments = arguments.ToList();
            CommandLine = commandline;
        }

        public async Task<ProcessStartInfo> ToProcessStartInfo() {
            ProcessStartInfo psi = new() {
                CreateNoWindow = CreateNoWindow,
                FileName = CommandLine,
                UseShellExecute = UseShellExecute,
                WindowStyle = WindowStyle
            };

            await Arguments.ForEachAsync(async a => {
                psi.ArgumentList.Add(a);
                await Task.CompletedTask;
            });

            return psi;
        }

        #region JsonConstructor
        public CommandModel() { }
        #endregion
    }
}
