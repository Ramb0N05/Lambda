using Newtonsoft.Json;
using System.ComponentModel;

namespace Lambda.Configuration {

    public class GeneralConfiguration : Configuration {

        [JsonProperty("advertiseNewGames", Required = Required.DisallowNull)]
        [DefaultValue(true)]
        public bool AdvertiseNewGames { get; set; } = true;

        [JsonProperty("autoPrepare", Required = Required.DisallowNull)]
        [DefaultValue(true)]
        public bool AutoPrepare { get; set; } = true;

        [JsonProperty("autoValidate", Required = Required.DisallowNull)]
        [DefaultValue(true)]
        public bool AutoValidate { get; set; } = true;

        [JsonProperty("defaultGameDirectoryIndex", Required = Required.Default)]
        public int? DefaultGameDirectoryIndex { get; set; }

        [JsonProperty("enableServer", Required = Required.DisallowNull)]
        [DefaultValue(true)]
        public bool EnableServer { get; set; } = true;

        [JsonProperty("gameDirectories", Required = Required.DisallowNull)]
        public string[] GameDirectories { get; set; } = [];

        [JsonProperty("ignoreFirstStart", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool IgnoreFirstStart { get; set; }

        [JsonProperty("useIPV4", Required = Required.DisallowNull)]
        [DefaultValue(false)]
        public bool UseIPv4 { get; set; }

        [JsonProperty("serverPort", Required = Required.Default)]
        [DefaultValue(1337)]
        public ushort ServerPort { get; set; } = 1337;

        public async Task<GeneralConfiguration> Get(string filePath, JsonSerializerSettings? jss = null)
            => await Get<GeneralConfiguration>(filePath, jss);
    }
}
