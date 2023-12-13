using Lambda.Generic;
using Lambda.Models;
using Lambda.Networking;
using Lambda.Networking.Events;
using Lambda.Networking.Zeroconf;
using Lambda.Views;
using Makaretu.Dns;
using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.ComponentModel;

namespace Lambda
{

    public partial class MainForm : Form {
        private ConfigurationManager _configManager;
        private readonly LambdaServer? _server;
        private readonly Zeroconf _zeroconf;
        private readonly BindingList<Game>? _gameList;

        public ImageList GameImages { get; private set; } = new ImageList();
        public ListViewGroup LocalListViewGroup { get; } = new("Local");
        public ListViewGroup RemoteListViewGroup { get; } = new("Remote");

        public MainForm() {
            _configManager = Program.ConfigManager ?? throw new NullReferenceException(nameof(ConfigurationManager));
            _server = Program.Server;
            _zeroconf = Program.Zeroconf ?? throw new NullReferenceException(nameof(Zeroconf));
            _gameList = _configManager.CurrentGameConfig.Games != null
                ? new BindingList<Game>(_configManager.CurrentGameConfig.Games.Select(g => Game.FromModel(g)).ToList())
                : [];

            InitializeComponent();
            GameImages.ImageSize = new Size(96, 96);
            lv_games.Groups.AddRange(new[] { LocalListViewGroup, RemoteListViewGroup });
            lv_games.LargeImageList = GameImages;
        }

        private async void mainForm_Load(object sender, EventArgs e) {
            Application.DoEvents();

            await loadGames();

            if (_server != null) {
                _server.OnMessageReceived += server_onMessageReceived_EventHandler;
                await _server.Listen();
            }
        }

        private async Task loadGames() {
            lv_games.Items.Clear();

            if (_gameList != null) {
                await _gameList.ForEachAsync(async game => {
                    if (!game.ImagePath.IsNull()) {
                        Image image = Image.FromFile(game.ImagePath);
                        GameImages.Images.Add(game.Identifier, image);
                    }

                    ListViewItem lvi = new(game.Name, game.Identifier, game.IsRemote ? LocalListViewGroup : RemoteListViewGroup) { Tag = game.Identifier };
                    lv_games.Items.Add(lvi);

                    await Task.CompletedTask;
                });
            }
        }

        private async void server_onMessageReceived_EventHandler(object? sender, MessageReceivedEventArgs e) {
            MessageBox.Show("Server received from '" + e.Message.SourceIdentifier + "': " + e.Message.Type.GetName() + Environment.NewLine
                + e.Message.PayloadJSON);

            if (e.ChannelHandlerContext != null) {
                switch (e.Message.Type) {
                    case LambdaMessageType.GetGames:
                        await e.ChannelHandlerContext.WriteAndFlushAsync(new LambdaMessage(
                            LambdaMessageType.GameInfo,
                            JsonConvert.SerializeObject(GameInformationModel.Create(_configManager.CurrentGameConfig.Games))
                        ));

                        break;

                    case LambdaMessageType.GetHost:
                        await e.ChannelHandlerContext.WriteAndFlushAsync(new LambdaMessage(
                            LambdaMessageType.HostInfo,
                            JsonConvert.SerializeObject(HostInformationModel.Create(Program.HostInformation))
                        ));

                        break;

                    default:
                        await e.ChannelHandlerContext.WriteAndFlushAsync(LambdaMessage.UnsupportedMessage);
                        break;
                }
            }
        }

        private async void client_onMessageReceived_EventHandler(object? sender, MessageReceivedEventArgs e) {
            MessageBox.Show("Client received from '" + e.Message.SourceIdentifier + "': " + e.Message.Type.GetName() + Environment.NewLine
                + e.Message.PayloadJSON);

            switch (e.Message.Type) {
                case LambdaMessageType.GameInfo:
                    GameInformationModel? gim = JsonConvert.DeserializeObject<GameInformationModel>(e.Message.PayloadJSON);

                    if (gim != null && _gameList != null) {
                        await gim.Games.ForEachAsync(async g => {
                            if (!_gameList.Any(gi => gi.Identifier == g.Identifier))
                                _gameList.Add(Game.FromModel(g));

                            await Task.CompletedTask;
                        });
                    }

                    await loadGames();
                    break;

                case LambdaMessageType.HostInfo:
                    HostInformationModel? him = JsonConvert.DeserializeObject<HostInformationModel>(e.Message.PayloadJSON);

                    break;

                default:
                    break;
            }
        }

        private void btn_settings_Click(object sender, EventArgs e)
            => new SettingsView(ref _configManager).ShowDialog();

        private async void btn_refresh_Click(object sender, EventArgs e) {
            Makaretu.Dns.Message query = new();
            query.Questions.Add(new Question { Name = _zeroconf.ServiceFQDN, Type = DnsType.ANY });

            using MulticastService mdns = new();
            mdns.Start();
            mdns.SendQuery(query);
            _ = await mdns.ResolveAsync(query);

            foreach (ServiceInstance i in _zeroconf.ServiceInstances.ToArray()) {
                try {
                    LambdaClient c = new(i.PrimaryEndPoint);
                    c.OnMessageReceived += client_onMessageReceived_EventHandler;
                    await c.Connect();
                    await c.Write(LambdaMessage.GetHostMessage);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void mainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (_server != null)
                await _server.Close();
        }

        private void btn_import_Click(object sender, EventArgs e) {
            InputBoxView inputBox = new("Import game", "Game directory:", true);
            inputBox.ExtraButtonClick += inputGameImportDir_ExtraButtonClick;

            if (inputBox.ShowDialog() == DialogResult.OK) {
                string? path = inputBox.UserInput;

                if (!path.IsNull() && Directory.Exists(path)) {
                    ProgressView<Game> pv = new("Hashing files ...");

                    pv.OnProgressFinished += progressView_OnProgressFinished;

                    Game.OnCreateHashesProgressChanged += (sender, e) => {
                        if (e.CurrentFileNumber == e.TotalFileCount && e.IsWritten) {
                            pv.UpdateProgress(101, "Hashing complete.");
                        } else {
                            pv.UpdateProgress(
                                e.TotalFileCount,
                                e.IsWritten ? e.CurrentFileNumber : e.CurrentFileNumber - 1,
                                "Hashing file (" + e.CurrentFileNumber + "/" + e.TotalFileCount + "):" + Environment.NewLine + e.CurrentFile.Name);
                        }
                    };

                    Task<Game> gameTask = Game.Import("test", "Test", path);
                    pv.ShowProgress(ref gameTask);
                }
            }
        }

        private void progressView_OnProgressFinished(object? sender, ProgressFinishedEventArgs<Game> e) {
            Game? g = e.Result;
        }

        private void inputGameImportDir_ExtraButtonClick(object? sender, InputBoxView.ExtraButtonClickEventArgs e) {
            FolderBrowserDialog fbd = new() {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ShowNewFolderButton = true
            };

            if (fbd.ShowDialog() == DialogResult.OK)
                e.InputBoxView.UpdateUserInput(fbd.SelectedPath);
        }
    }
}
