using DotNetty.Common.Internal.Logging;
using DotNetty.Transport.Channels;
using Lambda.Networking.Events;
using static Lambda.Networking.LambdaClientHandler;

namespace Lambda.Networking
{
    public class LambdaServerHandler : SimpleChannelInboundHandler<LambdaMessage> {
        static readonly IInternalLogger logger = InternalLoggerFactory.GetInstance<LambdaServerHandler>();

        public event EventHandler<EventArgs>? OnChannelClosed;
        public event EventHandler<EventArgs>? OnClientDisconnected;
        public event EventHandler<ExceptionCaughtEventArgs>? OnExceptionCaught;
        public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

        public override bool IsSharable => true;

        public override void ChannelActive(IChannelHandlerContext ctx) {
            // Detect when client disconnects
            ctx.Channel.CloseCompletion.ContinueWith((x) => {
                logger.Info("Channel Closed");
                OnChannelClosed?.Invoke(this, EventArgs.Empty);
            });
        }

        // The Channel is closed hence the connection is closed
        public override void ChannelInactive(IChannelHandlerContext ctx) {
            logger.Info("Client disconnected");
            OnClientDisconnected?.Invoke(this, EventArgs.Empty);
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, LambdaMessage message) {
            logger.Info("Received message: " + message);
            //ctx.WriteAndFlushAsync(message);
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(ctx, message));
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e) {
            logger.Error("Exception Caught: ", e);
            OnExceptionCaught?.Invoke(this, new ExceptionCaughtEventArgs(e));

            ctx.CloseAsync();
        }
    }
}
