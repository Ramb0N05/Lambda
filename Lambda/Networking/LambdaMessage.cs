using Lambda.Networking.Enumerations;
using SharpRambo.ExtensionsLib;

namespace Lambda.Networking {

    public class LambdaMessage(LambdaMessageType type, string sourceIdentifier, string payloadJson) {

        #region Public Fields

        public const int PROTOCOL_VERSION = 1;

        #endregion Public Fields

        #region Public Properties

        public static LambdaMessage GetGamesMessage { get; } = new(LambdaMessageType.GetGames, string.Empty);
        public static LambdaMessage GetHostMessage { get; } = new(LambdaMessageType.GetHost, string.Empty);
        public static LambdaMessage UnsupportedMessage { get; } = new(LambdaMessageType.Unsupported, string.Empty);
        public string PayloadJSON { get; set; } = payloadJson;
        public int ProtocolVersion { get; set; } = PROTOCOL_VERSION;
        public string SourceIdentifier { get; set; } = sourceIdentifier.IsNull() ? Program.HostInformation.Identifier : sourceIdentifier;
        public LambdaMessageType Type { get; set; } = type;

        #endregion Public Properties

        #region Public Constructors

        public LambdaMessage(LambdaMessageType type, string payloadJson) : this(type, string.Empty, payloadJson) {
        }

        #endregion Public Constructors
    }
}
