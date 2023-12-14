using Lambda.Generic;
using Lambda.Networking;
using Lambda.Networking.Zeroconf;

namespace Lambda {

    internal static class Program {

        #region Global Constants

        public const string SERVICE_NAME = "lambda";

        #endregion Global Constants

        #region Global Properties

        public static ConfigurationManager? ConfigManager { get; private set; }
        public static string ConfigPath { get; } = Path.Combine(Application.StartupPath, "config");
        public static HostInformation HostInformation { get; } = new();
        public static LambdaServer? Server { get; private set; }
        public static Zeroconf? Zeroconf { get; private set; }

        #endregion Global Properties

        #region Main Thread

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static async Task Main() {
            ConfigManager = await ConfigurationManager.Initialize(ConfigPath);
            using Zeroconf z = new(SERVICE_NAME);
            Zeroconf = z;

            if (ConfigManager.CurrentGeneralConfig.EnableServer) {
                Server = new(ConfigManager.CurrentGeneralConfig.ServerPort);
                Zeroconf.AdvertiseService(ConfigManager.CurrentGeneralConfig.ServerPort);
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        #endregion Main Thread
    }
}
