using Lambda.Views.Events;
using SharpRambo.ExtensionsLib;

namespace Lambda.Views {

    public partial class ProgressView<T> : View {

        #region Constants

        public const int PROGRESS_FINISHED = 101;
        public const int PROGRESS_MAX = 100;
        public const int PROGRESS_MIN = 0;

        public const int STATUS_LINE_HEIGHT = 15;
        public const int STATUS_LINE_PADDING = 2;

        #endregion Constants

        #region Private Fields

        private readonly int _initialHeight;
        private readonly int _initialWidth;

        private Task<T>? _startProgressTask;

        #endregion Private Fields

        #region Events

        public event EventHandler<ProgressFinishedEventArgs<T>>? OnProgressFinished;

        private event EventHandler<EventArgs>? OnLastProgressReached;

        #endregion Events

        #region Public Properties
        public bool CloseOnFinish { get; set; } = true;
        public CancellationToken FinishCancellationToken { get; } = new();
        public int FinishedDelay { get; set; } = 500;
        public T? Result { get; private set; }
        #endregion Public Properties

        #region Public Constructors

        public ProgressView() : this(TITLE_UNDEFINED) {
        }

        public ProgressView(string title) {
            InitializeComponent();

            if (lbl_status.Text == "<status>")
                lbl_status.Text = string.Empty;

            _initialHeight = Size.Height;
            _initialWidth = Size.Width;

            progressBar.Maximum = PROGRESS_MAX;
            progressBar.Minimum = PROGRESS_MIN;

            InitializeView();

            if (ViewForm != null) {
                ViewForm.Text = title;
                ViewForm.ControlBox = false;
                ViewForm.Shown += viewForm_Shown;
            }

            OnLastProgressReached += onLastProgressReached_EventHandler;
            OnProgressFinished += onProgressFinished_EventHandler;
        }

        #endregion Public Constructors

        #region Public Methods

        public void FinishProgress() => UpdateProgress(PROGRESS_FINISHED, string.Empty);

        public void FinishProgress(string status) => UpdateProgress(101, status);

        public void ShowProgress(ref Task<T> progressTask) {
            _startProgressTask = progressTask;
            ShowDialog();
        }

        public void UpdateProgress(int maxValue, int currentValue) => UpdateProgress(maxValue, currentValue, string.Empty);

        public void UpdateProgress(int maxValue, int currentValue, string status) {
            int percentage = (int)Math.Round((double)currentValue / maxValue * PROGRESS_MAX, 0);
            UpdateProgress(percentage, status);
        }

        public void UpdateProgress(int progressPercentage) => UpdateProgress(progressPercentage, string.Empty);

        public void UpdateProgress(int progressPercentage, string status) {
            if (progressPercentage == PROGRESS_FINISHED)
                OnLastProgressReached?.Invoke(this, EventArgs.Empty);

            progressPercentage = progressPercentage < PROGRESS_MIN ? PROGRESS_MIN : progressPercentage;
            progressPercentage = progressPercentage > PROGRESS_MAX ? PROGRESS_MAX : progressPercentage;

            progressBar.Value = progressPercentage;
            lbl_status.Text = status.Trim();
        }

        #endregion Public Methods

        #region Private Methods

        private void lbl_status_TextChanged(object sender, EventArgs e) {
            string[] lines = lbl_status.Text.Split(Environment.NewLine);
            int lineCount = !lines.LastOrDefault(string.Empty).IsNull() ? lines.Length : lines.Length - 1;

            int statusHeight = lineCount > 0
                ? (STATUS_LINE_HEIGHT * lineCount) + (STATUS_LINE_PADDING * 2)
                : 0;

            Size = new Size(_initialWidth, _initialHeight + statusHeight);
            tlp_main.RowStyles[tlp_main.GetRow(lbl_status)].Height = statusHeight;
        }

        private async void onLastProgressReached_EventHandler(object? sender, EventArgs e) {
            if (_startProgressTask?.IsCanceled == false && !_startProgressTask.IsCompleted && !_startProgressTask.IsCompletedSuccessfully && !_startProgressTask.IsFaulted)
                await _startProgressTask.WaitAsync(FinishCancellationToken);

            await Task.Delay(FinishedDelay);
            OnProgressFinished?.Invoke(this, new ProgressFinishedEventArgs<T>(Result));
        }

        private void onProgressFinished_EventHandler(object? sender, EventArgs e) {
            if (CloseOnFinish)
                Close();
        }

        private async void viewForm_Shown(object? sender, EventArgs e) {
            if (_startProgressTask != null) {
                Result = await _startProgressTask;
            }
        }

        #endregion Private Methods
    }
}
