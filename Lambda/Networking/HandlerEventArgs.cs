using DotNetty.Transport.Channels;

namespace Lambda.Networking {
    public class ExceptionCaughtEventArgs(Exception e, DateTime timestamp) : EventArgs {
        public Exception Exception { get; } = e;
        public DateTime Timestamp { get; } = timestamp;

        public ExceptionCaughtEventArgs(Exception e) : this(e, DateTime.Now) { }
    }

    public class MessageReceivedEventArgs(IChannelHandlerContext context, LambdaMessage message) : EventArgs {
        public IChannelHandlerContext ChannelHandlerContext { get; } = context;
        public LambdaMessage Message { get; } = message;
    }
}
