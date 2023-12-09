using SharpRambo.ExtensionsLib;
using System.ComponentModel;
using Lambda.Generic;

namespace Lambda.Views {
    public partial class SettingsView : View {
        private readonly ConfigurationManager configManager;
        private readonly BindingList<string> gameDirs = [];

        public SettingsView(ref ConfigurationManager configurationManager) {
            InitializeComponent();

            configManager = configurationManager;

            Initialize();
        }

        private async void SettingsView_Load(object sender, EventArgs e) {
            await configManager.GetConfig();

            int i = 0;
            await configManager.CurrentGeneralConfig.GameDirectories.ForEachAsync(async dir => {
                if (i == configManager.CurrentGeneralConfig.DefaultGameDirectoryIndex)
                    gameDirs.Add("*" + dir);
                else
                    gameDirs.Add(dir);

                i++;
                await Task.CompletedTask;
            });

            lb_gameDirectories.DataSource = gameDirs;
            cb_advertise.Checked = configManager.CurrentGeneralConfig.AdvertiseNewGames;
            cb_firstStart.Checked = configManager.CurrentGeneralConfig.IgnoreFirstStart;
            cb_prepare.Checked = configManager.CurrentGeneralConfig.AutoPrepare;
            cb_validate.Checked = configManager.CurrentGeneralConfig.AutoValidate;
            cb_server.Checked = configManager.CurrentGeneralConfig.EnableServer;
            nud_serverPort.Value = configManager.CurrentGeneralConfig.ServerPort;
            cb_useIPv4.Checked = configManager.CurrentGeneralConfig.UseIPv4;
        }

        private async void btn_save_Click(object sender, EventArgs e) {
            configManager.CurrentGeneralConfig.AdvertiseNewGames = cb_advertise.Checked;
            configManager.CurrentGeneralConfig.IgnoreFirstStart = cb_firstStart.Checked;
            configManager.CurrentGeneralConfig.AutoPrepare = cb_prepare.Checked;
            configManager.CurrentGeneralConfig.AutoValidate = cb_validate.Checked;
            configManager.CurrentGeneralConfig.EnableServer = cb_server.Checked;
            configManager.CurrentGeneralConfig.ServerPort = (ushort)nud_serverPort.Value;
            configManager.CurrentGeneralConfig.UseIPv4 = cb_useIPv4.Checked;

            List<string> dirs = [];

            for (int i = 0; i < lb_gameDirectories.Items.Count; i++) {
                object? item = lb_gameDirectories.Items[i];

                if (item is not null and string iStr && iStr.Length > 1) {
                    if (!iStr.StartsWith('*')) {
                        dirs.Add(iStr);
                        continue;
                    }

                    dirs.Add(iStr[1..]);
                    configManager.CurrentGeneralConfig.DefaultGameDirectoryIndex = i;
                }
            }

            configManager.CurrentGeneralConfig.GameDirectories = [.. dirs];
            await configManager.SaveConfig();
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e) {
            InputBoxView inputBox = new("Add game directory", "Type in the new directory path:", true);
            inputBox.ExtraButtonClick += inputGameDir_ExtraButtonClick;

            if (inputBox.ShowDialog() == DialogResult.OK) {
                string? path = inputBox.UserInput;

                if (!path.IsNull() && Directory.Exists(path))
                    gameDirs.Add(path);
            }
        }

        private void inputGameDir_ExtraButtonClick(object? sender, InputBoxView.ExtraButtonClickEventArgs e) {
            FolderBrowserDialog fbd = new() {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ShowNewFolderButton = true
            };

            if (fbd.ShowDialog() == DialogResult.OK)
                e.InputBoxView.UpdateUserInput(fbd.SelectedPath);
        }

        private void btn_edit_Click(object sender, EventArgs e) {
            if (lb_gameDirectories.SelectedIndex >= 0) {
                InputBoxView inputBox = new("Edit game directory", "Type in the new directory path:", true);
                string oldPath = lb_gameDirectories.SelectedItem?.ToString() ?? string.Empty;
                bool isDefault = false;

                if (oldPath.StartsWith('*')) {
                    oldPath = oldPath[1..];
                    isDefault = true;
                }

                inputBox.UpdateUserInput(oldPath);
                inputBox.ExtraButtonClick += inputGameDir_ExtraButtonClick;

                if (inputBox.ShowDialog() == DialogResult.OK) {
                    string? path = inputBox.UserInput;

                    if (!path.IsNull() && Directory.Exists(path) && lb_gameDirectories.SelectedIndex >= 0)
                        gameDirs[lb_gameDirectories.SelectedIndex] = isDefault ? "*" + path : path;
                }
            }
        }

        private void btn_remove_Click(object sender, EventArgs e) {
            if (lb_gameDirectories.SelectedIndex >= 0)
                gameDirs.RemoveAt(lb_gameDirectories.SelectedIndex);
        }

        private void btn_setDefault_Click(object sender, EventArgs e) {
            int sel = lb_gameDirectories.SelectedIndex;

            if (sel >= 0 && sel < gameDirs.Count) {
                for (int i = 0; i < gameDirs.Count; i++) {
                    if (i != sel
                        && gameDirs[i].Length > 1
                        && gameDirs[i].StartsWith('*')) {
                        gameDirs[i] = gameDirs[i][1..];
                    }
                }

                if (!gameDirs[sel].StartsWith('*'))
                    gameDirs[sel] = "*" + gameDirs[sel];
            }
        }

        private void btn_moveUp_Click(object sender, EventArgs e) {
            int sel = lb_gameDirectories.SelectedIndex;

            if (sel >= 0 && sel < lb_gameDirectories.Items.Count) {
                int moveTo = sel - 1;

                if (moveTo >= 0) {
                    (gameDirs[sel], gameDirs[moveTo]) = (gameDirs[moveTo], gameDirs[sel]);
                    lb_gameDirectories.SelectedIndex = moveTo;
                }
            }
        }

        private void btn_moveDown_Click(object sender, EventArgs e) {
            int sel = lb_gameDirectories.SelectedIndex;

            if (sel >= 0 && sel < lb_gameDirectories.Items.Count) {
                int moveTo = sel + 1;

                if (moveTo < gameDirs.Count) {
                    (gameDirs[sel], gameDirs[moveTo]) = (gameDirs[moveTo], gameDirs[sel]);
                    lb_gameDirectories.SelectedIndex = moveTo;
                }
            }
        }
    }
}
