using DotNetty.Common.Internal.Logging;
using DotNetty.Transport.Channels;
using Lambda.Networking.Events;

namespace Lambda.Networking {

    public class LambdaClientHandler : SimpleChannelInboundHandler<LambdaMessage> {

        #region Private Fields

        private static readonly IInternalLogger _logger = InternalLoggerFactory.GetInstance<LambdaServerHandler>();

        #endregion Private Fields

        #region Public Events

        public event EventHandler<ExceptionCaughtEventArgs>? OnExceptionCaught;

        public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

        #endregion Public Events

        #region Public Methods

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) {
            _logger.Error("Exception Caught: ", exception);
            OnExceptionCaught?.Invoke(this, new ExceptionCaughtEventArgs(exception));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(DateTime.Now.Millisecond);
            Console.WriteLine("{0}", exception.StackTrace);
            context.CloseAsync();
            Console.ResetColor();
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void ChannelRead0(IChannelHandlerContext ctx, LambdaMessage message) {
            _logger.Info("Received message: " + message);
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(ctx, message));

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Data returned from Server:");
            Console.WriteLine(message.ToString());
            Console.ResetColor();
        }

        #endregion Protected Methods
    }
}
