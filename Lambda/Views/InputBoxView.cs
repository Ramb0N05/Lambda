using Lambda.Views.Events;

namespace Lambda.Views {

    public partial class InputBoxView : View {

        #region Public Fields

        public event EventHandler<EventArgs> DataReady;
        public event EventHandler<ExtraButtonClickEventArgs>? ExtraButtonClick;

        #endregion Public Fields

        #region Public Properties

        public string? UserInput { get; private set; }

        #endregion Public Properties

        #region Public Constructors

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

            InitializeView();

            DataReady += dataReady_EventHandler;
            ExtraButton.Click += (sender, e) => ExtraButtonClick?.Invoke(sender, new ExtraButtonClickEventArgs(this));
            ViewClosed += (sender, e) => DataReady?.Invoke(sender, e);
        }

        #endregion Public Constructors

        #region Public Methods

        public void UpdateUserInput(string input)
            => tb_input.Text = input;

        #endregion Public Methods

        #region Private Methods

        private void dataReady_EventHandler(object? sender, EventArgs e)
            => UserInput = tb_input.Text;

        #endregion Private Methods
    }
}
