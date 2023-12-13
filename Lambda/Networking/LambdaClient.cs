using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Lambda.Networking.Events;
using System.Net;

namespace Lambda.Networking {

    public class LambdaClient {

        #region Private Fields

        private readonly Bootstrap _bootstrap = new();
        private readonly MultithreadEventLoopGroup _group = new();
        private IChannel? _bootstrapChannel;

        #endregion Private Fields

        #region Public Events

        public event EventHandler<ExceptionCaughtEventArgs>? OnExceptionCaught;

        public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

        #endregion Public Events

        #region Public Properties
        public IPAddress Address { get; }
        public ushort Port { get; }
        #endregion Public Properties

        #region Public Constructors

        public LambdaClient(IPEndPoint endpoint) : this(endpoint.Address, (ushort)endpoint.Port) {
        }

        public LambdaClient(IPAddress address, ushort port) {
            if (port == 0)
                throw new ArgumentOutOfRangeException(nameof(port), port, "Port must be greater than zero!");

            Address = address;
            Port = port;

            LambdaMessageDecoder decoder = new();
            LambdaMessageEncoder encoder = new();
            LambdaClientHandler clientHandler = new();

            clientHandler.OnExceptionCaught += (sender, e) => OnExceptionCaught?.Invoke(sender, e);
            clientHandler.OnMessageReceived += (sender, e) => OnMessageReceived?.Invoke(sender, e);

            try {
                _bootstrap
                    .Group(_group)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true) // Do not buffer and send packages right away
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel => {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(encoder, decoder, clientHandler);
                    }));
            } catch {
                //TODO
            }
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task Close() {
            if (_bootstrapChannel != null)
                await _bootstrapChannel.CloseAsync();

            await _group.ShutdownGracefullyAsync(new TimeSpan(0, 0, 2), new TimeSpan(0, 0, 3));
        }

        public async Task Connect()
            => _bootstrapChannel = await _bootstrap.ConnectAsync(new IPEndPoint(Address, Port));

        public async Task Write(LambdaMessage message) {
            if (_bootstrapChannel != null)
                await _bootstrapChannel.WriteAndFlushAsync(message);
        }

        #endregion Public Methods
    }
}
