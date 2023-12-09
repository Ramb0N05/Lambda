using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Net;

namespace Lambda.Networking {
    public class LambdaClient {
        private readonly Bootstrap _bootstrap = new();
        private IChannel _bootstrapChannel;
        private readonly MultithreadEventLoopGroup _group = new();

        public event EventHandler<ExceptionCaughtEventArgs>? OnExceptionCaught;
        public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

        public IPAddress Address { get; }
        public ushort Port { get; }

        public LambdaClient(IPEndPoint endpoint) : this(endpoint.Address, (ushort)endpoint.Port) { }
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

            }
        }

        public async Task Connect()
            => _bootstrapChannel = await _bootstrap.ConnectAsync(new IPEndPoint(Address, Port));

        public async Task Close() {
            await _bootstrapChannel.CloseAsync();
            await _group.ShutdownGracefullyAsync(new TimeSpan(0,0,2), new TimeSpan(0,0,3));
        }

        public async Task Write(LambdaMessage message)
            => await _bootstrapChannel.WriteAndFlushAsync(message);
    }
}
