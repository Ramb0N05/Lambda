using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Lambda.Generic;
using Lambda.Networking.Enumerations;
using SharpRambo.ExtensionsLib;
using System.Text;
using System.Text.RegularExpressions;

namespace Lambda.Networking {

    public partial class LambdaMessageDecoder : MessageToMessageDecoder<IByteBuffer> {

        #region Private Fields

        private readonly Encoding _encoding;

        #endregion Private Fields

        #region Public Properties

        public override bool IsSharable => true;

        #endregion Public Properties

        #region Public Constructors

        public LambdaMessageDecoder() : this(Encoding.GetEncoding(0)) {
        }

        public LambdaMessageDecoder(Encoding encoding) {
            _encoding = encoding ?? throw new NullReferenceException("encoding");
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output) {
            string text = message.ToString(_encoding);
            Match match = LambdaMessageRegex().Match(text);

            if (match.Success) {
                int protocolVersion = int.Parse(match.Groups["ProtocolVersion"].Value);

                if (protocolVersion <= LambdaMessage.PROTOCOL_VERSION) {
                    int type = int.Parse(match.Groups["Type"].Value);
                    LambdaMessageType typeE = Enum.IsDefined(typeof(LambdaMessageType), type) ? (LambdaMessageType)type : LambdaMessageType.GetGames;
                    string sourceIdentifier = match.Groups["SourceIdentifier"].Value;
                    string payload = match.Groups["Payload"].Value.FromBase64();

                    output.Add(new LambdaMessage(typeE, sourceIdentifier, payload));
                }
            }
        }

        #endregion Protected Methods

        #region Private Methods

        [GeneratedRegex(@"^(?<ProtocolVersion>\d+)\|(?<Type>\d+)\|(?<SourceIdentifier>\w+)\|(?<Payload>.+)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "de-DE")]
        private static partial Regex LambdaMessageRegex();

        #endregion Private Methods
    }
}
