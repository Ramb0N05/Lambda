using Newtonsoft.Json;
using System.ComponentModel;

namespace Lambda.Models {

    [JsonObject(MemberSerialization.OptIn)]
    public class GameModel {

        #region Basic

        [JsonProperty("identifier", Required = Required.Always)]
        public string Identifier { get; set; }

        [JsonProperty("imagePath", Required = Required.Default)]
        public string? ImagePath { get; set; }

        [JsonProperty("installLocation", Required = Required.Default)]
        public string? InstallLocation { get; set; }

        [JsonProperty("location", Required = Required.Always)]
        public string Location { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        #endregion Basic

        #region Additionals

        [JsonProperty("closedCommands", Required = Required.Default)]
        public List<CommandModel>? ClosedCommands { get; set; }

        [JsonProperty("description", Required = Required.Default)]
        public string? Description { get; set; }

        [JsonProperty("executeCommands", Required = Required.Default)]
        public List<CommandModel>? ExecuteCommands { get; set; }

        [JsonProperty("firstStartCommands", Required = Required.Default)]
        public List<CommandModel>? FirstStartCommands { get; set; }

        [JsonProperty("genres", Required = Required.Default)]
        public string[]? Genres { get; set; }

        [JsonProperty("installerCommands", Required = Required.Default)]
        public List<CommandModel>? InstallerCommands { get; set; }

        [JsonProperty("isExecuted", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IsExecuted { get; set; }

        [JsonProperty("isInstalled", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IsInstalled { get; set; }

        [JsonProperty("isPrepared", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IsPrepared { get; set; }

        [JsonProperty("isPublic", Required = Required.DisallowNull)]
        [DefaultValue(true)]
        public bool IsPublic { get; set; } = true;

        [JsonProperty("isRemote", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IsRemote { get; set; }

        [JsonProperty("isStandalone", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IsStandalone { get; set; }

        [JsonProperty("prepareCommands", Required = Required.Default)]
        public List<CommandModel>? PrepareCommands { get; set; }

        #endregion Additionals

        #region JSON Constructor

#pragma warning disable CS8618

        public GameModel() {
        }

#pragma warning restore CS8618

        #endregion JSON Constructor
    }
}
