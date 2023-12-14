namespace Lambda.Views.Events {

    public class ExtraButtonClickEventArgs(InputBoxView inputBoxView) : EventArgs {
        public InputBoxView InputBoxView { get; } = inputBoxView;
    }
}
