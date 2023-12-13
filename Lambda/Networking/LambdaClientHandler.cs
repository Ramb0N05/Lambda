using DotNetty.Common.Internal.Logging;
using DotNetty.Transport.Channels;
using Lambda.Networking.Events;

namespace Lambda.Networking
{
    public class LambdaClientHandler : SimpleChannelInboundHandler<LambdaMessage> {
        static readonly IInternalLogger logger = InternalLoggerFactory.GetInstance<LambdaServerHandler>();

        public event EventHandler<ExceptionCaughtEventArgs>? OnExceptionCaught;
        public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

        protected override void ChannelRead0(IChannelHandlerContext ctx, LambdaMessage message) {
            logger.Info("Received message: " + message);
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(ctx, message));

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Data returned from Server:");
            Console.WriteLine(message.ToString());
            Console.ResetColor();
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e) {
            logger.Error("Exception Caught: ", e);
            OnExceptionCaught?.Invoke(this, new ExceptionCaughtEventArgs(e));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(DateTime.Now.Millisecond);
            Console.WriteLine("{0}", e.StackTrace);
            ctx.CloseAsync();
            Console.ResetColor();
        }
    }
}
