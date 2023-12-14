using DotNetty.Common.Internal.Logging;
using DotNetty.Transport.Channels;
using Lambda.Networking.Events;

namespace Lambda.Networking {

    public class LambdaServerHandler : SimpleChannelInboundHandler<LambdaMessage> {

        #region Private Fields

        private static readonly IInternalLogger _logger = InternalLoggerFactory.GetInstance<LambdaServerHandler>();

        #endregion Private Fields

        #region Public Events

        public event EventHandler<EventArgs>? OnChannelClosed;

        public event EventHandler<EventArgs>? OnClientDisconnected;

        public event EventHandler<ExceptionCaughtEventArgs>? OnExceptionCaught;

        public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

        #endregion Public Events

        #region Public Properties

        public override bool IsSharable => true;

        #endregion Public Properties

        #region Public Methods

        public override void ChannelActive(IChannelHandlerContext context) =>
            context.Channel.CloseCompletion.ContinueWith((_) => {
                _logger.Info("Channel Closed");
                OnChannelClosed?.Invoke(this, EventArgs.Empty);
            });

        // The Channel is closed hence the connection is closed
        public override void ChannelInactive(IChannelHandlerContext context) {
            _logger.Info("Client disconnected");
            OnClientDisconnected?.Invoke(this, EventArgs.Empty);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) {
            _logger.Error("Exception Caught: ", exception);
            OnExceptionCaught?.Invoke(this, new ExceptionCaughtEventArgs(exception));

            context.CloseAsync();
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void ChannelRead0(IChannelHandlerContext ctx, LambdaMessage message) {
            _logger.Info("Received message: " + message);
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(ctx, message));
        }

        #endregion Protected Methods
    }
}
