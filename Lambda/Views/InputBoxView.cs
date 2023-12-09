using System;
namespace Lambda.Views
{
    public partial class InputBoxView : View {
        public class ExtraButtonClickEventArgs(InputBoxView inputBoxView) : EventArgs {
            public InputBoxView InputBoxView { get; } = inputBoxView;
        }

        public delegate void DataReadyHandler(object sender, EventArgs e);
        public EventHandler<EventArgs> DataReady;

        public delegate void ExtraButtonClickHandler(object sender, ExtraButtonClickEventArgs e);
        public EventHandler<ExtraButtonClickEventArgs>? ExtraButtonClick;

        public string? UserInput { get; private set; }

        public InputBoxView(string title, string message, bool showExtraButton = false) {
            InitializeComponent();

            Title = title;
            lbl_message.Text = message;
            ExtraButton.Visible = showExtraButton;

            if (showExtraButton) {
                TableLayoutPanelCellPosition cancelCell = tlp_main.GetCellPosition(btn_cancel);
                TableLayoutPanelCellPosition okCell = tlp_main.GetCellPosition(btn_ok);

                cancelCell.Column++;
                okCell.Column++;

                tlp_main.SetCellPosition(btn_cancel, cancelCell);
                tlp_main.SetCellPosition(btn_ok, okCell);
            }

            Initialize();

            DataReady += DataReady_EventHandler;
            ExtraButton.Click += (sender, e) => ExtraButtonClick?.Invoke(sender, new ExtraButtonClickEventArgs(this));
            ViewClosed += (sender, e) => DataReady?.Invoke(sender, e);
        }

        public void UpdateUserInput(string input)
            => tb_input.Text = input;

        private void DataReady_EventHandler(object? sender, EventArgs e)
            => UserInput = tb_input.Text;
    }
}
