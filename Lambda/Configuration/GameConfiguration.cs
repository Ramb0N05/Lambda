using Lambda.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Lambda.Configuration {

    [JsonObject(MemberSerialization.OptIn)]
    public class GameConfiguration : Configuration {

        [JsonProperty("games", Required = Required.AllowNull)]
        public BindingList<GameModel>? Games { get; set; }

        public async Task<GameConfiguration> Get(string filePath, JsonSerializerSettings? jss = null)
            => await Get<GameConfiguration>(filePath, jss);
    }
}
