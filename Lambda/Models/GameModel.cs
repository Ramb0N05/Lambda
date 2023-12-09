using Newtonsoft.Json;
using System.ComponentModel;

namespace Lambda.Models {
    [JsonObject(MemberSerialization.OptIn)]
    public class GameModel {
        #region Basic
        [JsonProperty("identifier", Required = Required.Always)]
        public string Identifier { get; set; }

        [JsonProperty("imagePath", Required = Required.Default)]
        public string? ImagePath { get;set; }

        [JsonProperty("location", Required = Required.Always)]
        public string Location { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
        #endregion

        #region Additionals
        [JsonProperty("description", Required = Required.Default)]
        public string? Description { get; set; }

        [JsonProperty("executeCommands", Required = Required.Default)]
        public string[]? ExecuteCommands { get; set; }

        [JsonProperty("firstStartCommands", Required = Required.Default)]
        public string[]? FirstStartCommands { get; set; }

        [JsonProperty("genres", Required = Required.Default)]
        public string[]? Genres { get; set; }

        [JsonProperty("installerCommands", Required = Required.Default)]
        public string[]? InstallerCommands { get; set; }

        [JsonProperty("isExecuted", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IsExecuted { get; set; }

        [JsonProperty("isPrepared", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IsPrepared { get; set; }

        [JsonProperty("isPublic", Required = Required.DisallowNull)]
        [DefaultValue(true)]
        public bool IsPublic { get; set; }

        [JsonProperty("isStandalone", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IsStandalone { get; set; }

        [JsonProperty("prepareCommands", Required = Required.Default)]
        public string[]? PrepareCommands { get; set; }
        #endregion

        #region JSON Constructor
        public GameModel() { }
        #endregion
    }
}
