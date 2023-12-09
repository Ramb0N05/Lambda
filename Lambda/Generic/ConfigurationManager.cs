using Lambda.Configuration;
using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;

namespace Lambda.Generic {
    public class ConfigurationManager {
        const string GeneralFileName = "config.json";
        const string GameFileName = "games.json";

        private string generalFilePath;
        private string gameFilePath;

        public JsonSerializerSettings JSS = new() { Formatting = Formatting.Indented };
        public GeneralConfiguration CurrentGeneralConfig { get; private set; }
        public GameConfiguration CurrentGameConfig { get; private set; }

        private ConfigurationManager(string configDir) {
            generalFilePath = Path.Combine(configDir, GeneralFileName);
            gameFilePath = Path.Combine(configDir, GameFileName);

            CurrentGeneralConfig = new();
            CurrentGameConfig = new();
        }

        public static async Task<ConfigurationManager> Initialize(string configDirectory) {
            DirectoryInfo configDir = new(configDirectory);
            ConfigurationManager configManager = new(configDir.FullName);

            string generalFilePath = Path.Combine(configDir.FullName, GeneralFileName);
            string gameFilePath = Path.Combine(configDir.FullName, GameFileName);

            if (!configDir.Exists) {
                configDir.CreateAnyway();
                await configManager.SaveConfig();
            } else {
                if (!File.Exists(generalFilePath) && !await configManager.CurrentGeneralConfig.Save(generalFilePath, configManager.JSS))
                    throw new Exception("Could not save general config file (\"" + generalFilePath + "\")");

                if (!File.Exists(gameFilePath) && !await configManager.CurrentGameConfig.Save(gameFilePath, configManager.JSS))
                    throw new Exception("Could not save game config file (\"" + generalFilePath + "\")");

                await configManager.GetConfig();
            }

            return configManager;
        }

        public async Task<bool> SaveConfig()
            => (await CurrentGeneralConfig.Save(generalFilePath, JSS)) && await CurrentGameConfig.Save(gameFilePath, JSS);

        public async Task GetConfig(){
            CurrentGeneralConfig = await CurrentGeneralConfig.Get(generalFilePath, JSS);
            CurrentGameConfig = await CurrentGameConfig.Get(gameFilePath, JSS);
        }
    }
}
