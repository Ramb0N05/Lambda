using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using SharpRambo.ExtensionsLib;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lambda.Networking {
    public partial class LambdaMessageDecoder : MessageToMessageDecoder<IByteBuffer> {
        private readonly Encoding _encoding;
        public override bool IsSharable => true;

        public LambdaMessageDecoder() : this(Encoding.GetEncoding(0)) {}

        public LambdaMessageDecoder(Encoding encoding) {
            _encoding = encoding ?? throw new NullReferenceException("encoding");
        }

        [GeneratedRegex(@"^(?<ProtocolVersion>\d+)\|(?<Type>\d+)\|(?<SourceIdentifier>\w+)\|(?<Payload>.+)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "de-DE")]
        private static partial Regex LambdaMessageRegex();

        protected override void Decode(IChannelHandlerContext ctx, IByteBuffer message, List<object> output) {
            string text = message.ToString(_encoding);
            Match match = LambdaMessageRegex().Match(text);

            if (match.Success) {
                int protocolVersion = int.Parse(match.Groups["ProtocolVersion"].Value);

                if (protocolVersion <= LambdaMessage.PROTOCOL_VERSION) {
                    int type = int.Parse(match.Groups["Type"].Value);
                    LambdaMessageType typeE = Enum.IsDefined(typeof(LambdaMessageType), type) ? (LambdaMessageType)type : LambdaMessageType.GetGames;
                    string sourceIdentifier = match.Groups["SourceIdentifier"].Value;
                    string payload = fromBase64(match.Groups["Payload"].Value);

                    output.Add(new LambdaMessage(typeE, sourceIdentifier, payload));
                }
            }
        }

        private string fromBase64(string message) {
            if (message.IsNull())
                return string.Empty;

            ReadOnlySpan<byte> b64Bytes = Encoding.UTF8.GetBytes(message);
            Span<byte> bytes = [];

            return Base64.DecodeFromUtf8(b64Bytes, bytes, out _, out _) == System.Buffers.OperationStatus.Done
                ? Encoding.UTF8.GetString(bytes)
                : string.Empty;
        }
    }
}
