using Newtonsoft.Json;

namespace Lambda.Models {
    [JsonObject(MemberSerialization.OptIn)]
    public class GameInformationModel {
        [JsonProperty("games", Required = Required.Always)]
        public List<GameModel> Games { get; set; } = [];

        public static GameInformationModel Create(IEnumerable<GameModel>? games)
            => games != null ? new() { Games = new List<GameModel>(games) } : new();
    }
}
