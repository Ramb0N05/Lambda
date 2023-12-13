using Lambda.Generic;
using SharpRambo.ExtensionsLib;
using System.Runtime.CompilerServices;

namespace Lambda.Views {
    public class ProgressFinishedEventArgs<T>(T? result) : EventArgs {
        public T? Result { get; } = result;
    }

    public partial class ProgressView<T> : View {
        public const int PROGRESS_FINISHED = 101;
        public const int PROGRESS_MAX = 100;
        public const int PROGRESS_MIN = 0;

        public const int STATUS_LINE_HEIGHT = 15;
        public const int STATUS_LINE_PADDING = 2;

        private int initialHeight;
        private int initialWidth;

        private Task<T>? startProgressTask;

        private event EventHandler<EventArgs>? OnLastProgressReached;
        public event EventHandler<ProgressFinishedEventArgs<T>>? OnProgressFinished;

        public bool CloseOnFinish { get; set; } = true;
        public CancellationToken FinishCancellationToken { get; } = new();
        public int FinishedDelay { get; set; } = 500;
        public T? Result { get; private set; }

        public ProgressView() : this(TITLE_UNDEFINED) { }
        public ProgressView(string title) {
            InitializeComponent();

            if (lbl_status.Text == "<status>")
                lbl_status.Text = string.Empty;

            initialHeight = Size.Height;
            initialWidth = Size.Width;

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

        public void ShowProgress(ref Task<T> progressTask) {
            startProgressTask = progressTask;
            ShowDialog();
        }

        public void FinishProgress() => UpdateProgress(PROGRESS_FINISHED, string.Empty);
        public void FinishProgress(string status) => UpdateProgress(101, status);

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

        private void lbl_status_TextChanged(object sender, EventArgs e) {
            string[] lines = lbl_status.Text.Split(Environment.NewLine);
            int lineCount = !lines.LastOrDefault(string.Empty).IsNull() ? lines.Length : lines.Length - 1;

            int statusHeight = lineCount > 0
                ? (STATUS_LINE_HEIGHT * lineCount) + (STATUS_LINE_PADDING * 2)
                : 0;

            Size = new Size(initialWidth, initialHeight + statusHeight);
            tlp_main.RowStyles[tlp_main.GetRow(lbl_status)].Height = statusHeight;
        }

        private async void viewForm_Shown(object? sender, EventArgs e) {
            if (startProgressTask != null) {
                Result = await startProgressTask;
            }
        }

        private async void onLastProgressReached_EventHandler(object? sender, EventArgs e) {
            if (startProgressTask?.IsCanceled == false && !startProgressTask.IsCompleted && !startProgressTask.IsCompletedSuccessfully && !startProgressTask.IsFaulted)
                await startProgressTask.WaitAsync(FinishCancellationToken);

            await Task.Delay(FinishedDelay);
            OnProgressFinished?.Invoke(this, new ProgressFinishedEventArgs<T>(Result));
        }

        private void onProgressFinished_EventHandler(object? sender, EventArgs e) {
            if (CloseOnFinish)
                Close();
        }
    }
}
