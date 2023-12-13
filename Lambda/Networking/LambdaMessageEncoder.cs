using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using SharpRambo.ExtensionsLib;
using System.Buffers.Text;
using System.Text;

namespace Lambda.Networking {
    public class LambdaMessageEncoder : MessageToMessageEncoder<LambdaMessage> {
        private readonly Encoding _encoding;
        public override bool IsSharable => true;

        public LambdaMessageEncoder() : this(Encoding.GetEncoding(0)) { }

        public LambdaMessageEncoder(Encoding encoding) {
            _encoding = encoding ?? throw new NullReferenceException("encoding");
        }

        protected override void Encode(IChannelHandlerContext ctx, LambdaMessage message, List<object> output) {
            if (message is null)
                return;

            string payloadB64 = string.Empty;

            if (!message.PayloadJSON.IsNull())
                payloadB64 = toBase64(message.PayloadJSON);

            string msg = $"{message.ProtocolVersion}|{(int)message.Type}|{message.SourceIdentifier}|{payloadB64}";
            output.Add(ByteBufferUtil.EncodeString(ctx.Allocator, msg, _encoding));
        }

        private string toBase64(string message) {
            if (message.IsNull())
                return string.Empty;

            ReadOnlySpan<byte> bytes = Encoding.UTF8.GetBytes(message);
            return System.Convert.ToBase64String(bytes);
        }
    }
}
