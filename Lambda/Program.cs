using Lambda.Generic;
using Lambda.Networking;
using Lambda.Networking.Zeroconf;

namespace Lambda
{
    internal static class Program {
        public const string MDNS_SERVICE_NAME = "lambda";

        public static ConfigurationManager? ConfigManager { get; private set; }
        public static string ConfigPath { get; } = Path.Combine(Application.StartupPath, "config");
        public static HostInformation HostInformation { get; } = new();
        public static LambdaServer? Server { get; private set; }
        public static Zeroconf? Zeroconf { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main() {
            ConfigManager = await ConfigurationManager.Initialize(ConfigPath);
            using Zeroconf z = new(MDNS_SERVICE_NAME);
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
    }
}