using SharpRambo.ExtensionsLib;
using System.ComponentModel;
using Lambda.Generic;
using Lambda.Views.Events;

namespace Lambda.Views {

    public partial class SettingsView : View {
        private readonly ConfigurationManager _configManager;
        private readonly BindingList<string> _gameDirs = [];

        public SettingsView(ref ConfigurationManager configurationManager) {
            InitializeComponent();

            _configManager = configurationManager;

            InitializeView();
        }

        private async void settingsView_Load(object sender, EventArgs e) {
            await _configManager.GetConfig();

            int i = 0;
            await _configManager.CurrentGeneralConfig.GameDirectories.ForEachAsync(async dir => {
                if (i == _configManager.CurrentGeneralConfig.DefaultGameDirectoryIndex)
                    _gameDirs.Add("*" + dir);
                else
                    _gameDirs.Add(dir);

                i++;
                await Task.CompletedTask;
            });

            lb_gameDirectories.DataSource = _gameDirs;
            cb_advertise.Checked = _configManager.CurrentGeneralConfig.AdvertiseNewGames;
            cb_firstStart.Checked = _configManager.CurrentGeneralConfig.IgnoreFirstStart;
            cb_prepare.Checked = _configManager.CurrentGeneralConfig.AutoPrepare;
            cb_validate.Checked = _configManager.CurrentGeneralConfig.AutoValidate;
            cb_server.Checked = _configManager.CurrentGeneralConfig.EnableServer;
            nud_serverPort.Value = _configManager.CurrentGeneralConfig.ServerPort;
            cb_useIPv4.Checked = _configManager.CurrentGeneralConfig.UseIPv4;
        }

        private async void btn_save_Click(object sender, EventArgs e) {
            _configManager.CurrentGeneralConfig.AdvertiseNewGames = cb_advertise.Checked;
            _configManager.CurrentGeneralConfig.IgnoreFirstStart = cb_firstStart.Checked;
            _configManager.CurrentGeneralConfig.AutoPrepare = cb_prepare.Checked;
            _configManager.CurrentGeneralConfig.AutoValidate = cb_validate.Checked;
            _configManager.CurrentGeneralConfig.EnableServer = cb_server.Checked;
            _configManager.CurrentGeneralConfig.ServerPort = (ushort)nud_serverPort.Value;
            _configManager.CurrentGeneralConfig.UseIPv4 = cb_useIPv4.Checked;

            List<string> dirs = [];

            for (int i = 0; i < lb_gameDirectories.Items.Count; i++) {
                object? item = lb_gameDirectories.Items[i];

                if (item is string iStr && iStr.Length > 1) {
                    if (!iStr.StartsWith('*')) {
                        dirs.Add(iStr);
                        continue;
                    }

                    dirs.Add(iStr[1..]);
                    _configManager.CurrentGeneralConfig.DefaultGameDirectoryIndex = i;
                }
            }

            _configManager.CurrentGeneralConfig.GameDirectories = [.. dirs];
            await _configManager.SaveConfig();
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e) {
            InputBoxView inputBox = new("Add game directory", "Type in the new directory path:", true);
            inputBox.ExtraButtonClick += inputGameDir_ExtraButtonClick;

            if (inputBox.ShowDialog() == DialogResult.OK) {
                string? path = inputBox.UserInput;

                if (!path.IsNull() && Directory.Exists(path))
                    _gameDirs.Add(path);
            }
        }

        private void inputGameDir_ExtraButtonClick(object? sender, ExtraButtonClickEventArgs e) {
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
                        _gameDirs[lb_gameDirectories.SelectedIndex] = isDefault ? "*" + path : path;
                }
            }
        }

        private void btn_remove_Click(object sender, EventArgs e) {
            if (lb_gameDirectories.SelectedIndex >= 0)
                _gameDirs.RemoveAt(lb_gameDirectories.SelectedIndex);
        }

        private void btn_setDefault_Click(object sender, EventArgs e) {
            int sel = lb_gameDirectories.SelectedIndex;

            if (sel >= 0 && sel < _gameDirs.Count) {
                for (int i = 0; i < _gameDirs.Count; i++) {
                    if (i != sel
                        && _gameDirs[i].Length > 1
                        && _gameDirs[i].StartsWith('*')) {
                        _gameDirs[i] = _gameDirs[i][1..];
                    }
                }

                if (!_gameDirs[sel].StartsWith('*'))
                    _gameDirs[sel] = "*" + _gameDirs[sel];
            }
        }

        private void btn_moveUp_Click(object sender, EventArgs e) {
            int sel = lb_gameDirectories.SelectedIndex;

            if (sel >= 0 && sel < lb_gameDirectories.Items.Count) {
                int moveTo = sel - 1;

                if (moveTo >= 0) {
                    (_gameDirs[sel], _gameDirs[moveTo]) = (_gameDirs[moveTo], _gameDirs[sel]);
                    lb_gameDirectories.SelectedIndex = moveTo;
                }
            }
        }

        private void btn_moveDown_Click(object sender, EventArgs e) {
            int sel = lb_gameDirectories.SelectedIndex;

            if (sel >= 0 && sel < lb_gameDirectories.Items.Count) {
                int moveTo = sel + 1;

                if (moveTo < _gameDirs.Count) {
                    (_gameDirs[sel], _gameDirs[moveTo]) = (_gameDirs[moveTo], _gameDirs[sel]);
                    lb_gameDirectories.SelectedIndex = moveTo;
                }
            }
        }
    }
}
