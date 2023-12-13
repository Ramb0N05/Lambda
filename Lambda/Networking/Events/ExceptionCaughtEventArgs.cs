namespace Lambda.Networking.Events {

    public class ExceptionCaughtEventArgs(Exception e, DateTime timestamp) : EventArgs {
        public Exception Exception { get; } = e;
        public DateTime Timestamp { get; } = timestamp;

        public ExceptionCaughtEventArgs(Exception e) : this(e, DateTime.Now) {
        }
    }
}
