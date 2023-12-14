using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Lambda.Generic;
using SharpRambo.ExtensionsLib;
using System.Text;

namespace Lambda.Networking {

    public class LambdaMessageEncoder(Encoding encoding) : MessageToMessageEncoder<LambdaMessage> {
        private readonly Encoding _encoding = encoding ?? throw new NullReferenceException("encoding");
        public override bool IsSharable => true;

        public LambdaMessageEncoder() : this(Encoding.GetEncoding(0)) {
        }

        protected override void Encode(IChannelHandlerContext context, LambdaMessage message, List<object> output) {
            if (message is null)
                return;

            string payloadB64 = string.Empty;

            if (!message.PayloadJSON.IsNull())
                payloadB64 = message.PayloadJSON.ToBase64();

            string msg = $"{message.ProtocolVersion}|{(int)message.Type}|{message.SourceIdentifier}|{payloadB64}";
            output.Add(ByteBufferUtil.EncodeString(context.Allocator, msg, _encoding));
        }
    }
}
