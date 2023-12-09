using Lambda.Models;

namespace Lambda.Generic {
    public class Game : GameModel {
        #region Properties
        public string RemoteLocation { get; private set; }
        #endregion

        #region Construct
        public Game(string identifier, string name, string location) {
            Identifier = identifier;
            Name = name;
            Location = location;
        }

        public static async Task<Game> FromRemote() {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods
        public async Task<int> Execute() {
            throw new NotImplementedException();
        }

        public async Task<bool> Fetch() {
            throw new NotImplementedException();
        }

        public async Task<bool> Install() {
            throw new NotImplementedException();
        }

        public async Task<bool> Prepare() {
            throw new NotImplementedException();
        }

        public async Task<bool> Validate() {
            throw new NotImplementedException();
        }
        #endregion
    }
}
