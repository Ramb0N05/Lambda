using Lambda.Generic;
using Lambda.Models;
using Lambda.Networking;
using Lambda.Networking.Zeroconf;
using Lambda.Views;
using Makaretu.Dns;
using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.Net;

namespace Lambda
{
    public partial class MainForm : Form {
        private ConfigurationManager _configManager;
        private readonly LambdaServer? _server;
        private readonly Zeroconf _zeroconf;

        public ImageList GameImages { get; private set; } = new ImageList();
        public ListViewGroup LVG_Local = new("Local");
        public ListViewGroup LVG_Remote = new("Remote");

        public MainForm() {
            _configManager = Program.ConfigManager ?? throw new NullReferenceException(nameof(ConfigurationManager));
            _server = Program.Server;
            _zeroconf = Program.Zeroconf ?? throw new NullReferenceException(nameof(Zeroconf));

            InitializeComponent();
            GameImages.ImageSize = new Size(96, 96);
            lv_games.Groups.AddRange(new[] { LVG_Local, LVG_Remote });
            lv_games.LargeImageList = GameImages;
        }

        private async void MainForm_Load(object sender, EventArgs e) {
            Application.DoEvents();

            await initializeGames();

            if (_server != null) {
                _server.OnMessageReceived += server_onMessageReceived_EventHandler;
                await _server.Listen();
            }
        }

        private async Task initializeGames() {
            lv_games.Items.Clear();

            if (_configManager.CurrentGameConfig.Games != null) {
                await _configManager.CurrentGameConfig.Games.ForEachAsync(async game => {
                    if (!game.ImagePath.IsNull()) {
                        Image image = Image.FromFile(game.ImagePath);
                        GameImages.Images.Add(game.Identifier, image);
                    }

                    ListViewItem lvi = new(game.Name, game.Identifier, LVG_Local) { Tag = game.Identifier };
                    lv_games.Items.Add(lvi);

                    await Task.CompletedTask;
                });
            }
        }

        private async void server_onMessageReceived_EventHandler(object? sender, MessageReceivedEventArgs e) {
            MessageBox.Show("Server received from '" + e.Message.SourceIdentifier + "': " + e.Message.Type.GetName() + Environment.NewLine
                + e.Message.PayloadJSON);

            switch (e.Message.Type) {
                case LambdaMessageType.GetGames:

                    break;

                case LambdaMessageType.GetHost:
                    if (e.ChannelHandlerContext != null) {
                        await e.ChannelHandlerContext.WriteAsync(new LambdaMessage(
                            LambdaMessageType.HostInfo,
                            JsonConvert.SerializeObject(HostInformationModel.Create(Program.HostInformation))
                        ));
                    }

                    break;
            }
        }

        private void client_onMessageReceived_EventHandler(object? sender, MessageReceivedEventArgs e) {
            MessageBox.Show("Client received from '" + e.Message.SourceIdentifier + "': " + e.Message.Type.GetName() + Environment.NewLine
                + e.Message.PayloadJSON);
        }

        private void btn_settings_Click(object sender, EventArgs e)
            => new SettingsView(ref _configManager).ShowDialog();

        private async void btn_refresh_Click(object sender, EventArgs e) {
            /*LambdaMessage msg = new(LambdaMessageType.HostInfo, string.Empty);
            await Client.Write(msg);*/

            Makaretu.Dns.Message query = new();
            query.Questions.Add(new Question { Name = _zeroconf.ServiceFQDN, Type = DnsType.ANY });

            using MulticastService mdns = new();
            mdns.Start();
            mdns.SendQuery(query);
            await mdns.ResolveAsync(query);

            foreach (ServiceInstance i in _zeroconf.ServiceInstances.ToArray()) {
                try {
                    string ipMsg = "All EndPoints:" + Environment.NewLine;

                    foreach (IPEndPoint ep in i.IPEndPoints)
                        ipMsg += ep + "; ";

                    ipMsg += Environment.NewLine + Environment.NewLine + "Primary EndPoint:" + Environment.NewLine;
                    ipMsg += i.PrimaryEndPoint.ToString();

                    MessageBox.Show(i.InstanceName + Environment.NewLine + ipMsg);

                    LambdaClient c = new(i.PrimaryEndPoint);
                    await c.Connect();
                    await c.Write(new LambdaMessage(LambdaMessageType.GetHost, string.Empty));
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (_server != null)
                await _server.Close();
        }

        private async void MainForm_Shown(object sender, EventArgs e) {

        }
    }
}
