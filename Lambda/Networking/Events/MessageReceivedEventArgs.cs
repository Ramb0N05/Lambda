using DotNetty.Transport.Channels;

namespace Lambda.Networking.Events {

    public class MessageReceivedEventArgs(IChannelHandlerContext context, LambdaMessage message) : EventArgs {
        public IChannelHandlerContext ChannelHandlerContext { get; } = context;
        public LambdaMessage Message { get; } = message;
    }
}
