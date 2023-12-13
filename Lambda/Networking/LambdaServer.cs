using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Lambda.Networking.Events;
using System.Net;

namespace Lambda.Networking
{
    public class LambdaServer {
        private readonly ServerBootstrap _bootstrap = new();
        private IChannel? _bootstrapChannel;
        private readonly MultithreadEventLoopGroup _bossGroup = new(1);  // accepts an incoming connection
        private readonly MultithreadEventLoopGroup _workerGroup = new(); // handles the traffic of the accepted connection once the boss accepts the connection and registers the accepted connection to the worker

        public event EventHandler<EventArgs>? OnChannelClosed;
        public event EventHandler<EventArgs>? OnClientDisconnected;
        public event EventHandler<ExceptionCaughtEventArgs>? OnExceptionCaught;
        public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

        public IPAddress? IP { get; }
        public ushort Port { get; }

        public LambdaServer(ushort port) : this(null, port) { }
        public LambdaServer(IPAddress? ip, ushort port) {
            if (port == 0)
                throw new ArgumentOutOfRangeException(nameof(port), port, "Port must be greater than zero!");

            IP = ip ?? IPAddress.Any;
            Port = port;
            const LogLevel logLevel = LogLevel.INFO;

            LambdaMessageDecoder decoder = new();
            LambdaMessageEncoder encoder = new();
            LambdaServerHandler serverHandler = new();

            serverHandler.OnChannelClosed += (sender, e) => OnChannelClosed?.Invoke(sender, e);
            serverHandler.OnClientDisconnected += (sender, e) => OnClientDisconnected?.Invoke(sender, e);
            serverHandler.OnExceptionCaught += (sender, e) => OnExceptionCaught?.Invoke(sender, e);
            serverHandler.OnMessageReceived += (sender, e) => OnMessageReceived?.Invoke(sender, e);

            try {
                _bootstrap
                    .Group(_bossGroup, _workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100) // maximum queue length for incoming connection
                    .Handler(new LoggingHandler(logLevel))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel => {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(encoder, decoder, serverHandler);
                    }));
            } catch {

            }
        }

        public async Task Listen()
            => _bootstrapChannel = IP != null ? await _bootstrap.BindAsync(IP, Port) : await _bootstrap.BindAsync(Port);

        public async Task Close() {
            if (_bootstrapChannel != null)
            await _bootstrapChannel.CloseAsync();

            await _bossGroup.ShutdownGracefullyAsync();
            await _workerGroup.ShutdownGracefullyAsync();
        }

        public async Task Write(LambdaMessage message) {
            if (_bootstrapChannel != null)
                await _bootstrapChannel.WriteAndFlushAsync(message);
        }
    }
}
