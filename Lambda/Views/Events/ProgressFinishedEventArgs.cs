namespace Lambda.Views.Events {

    public class ProgressFinishedEventArgs<T>(T? result) : EventArgs {
        public T? Result { get; } = result;
    }
}
