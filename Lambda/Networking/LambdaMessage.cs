using Lambda.Generic;
using SharpRambo.ExtensionsLib;
using System.Management;

namespace Lambda.Networking {
    public enum LambdaMessageType {
        Unsupported = 0,

        GetGames = 10,
        GameInfo = 11,

        GetHost = 20,
        HostInfo = 21,
    }

    public class LambdaMessage(LambdaMessageType type, string sourceIdentifier, string payloadJson) {
        public const int PROTOCOL_VERSION = 1;

        public static LambdaMessage UnsupportedMessage { get; } = new(LambdaMessageType.Unsupported, string.Empty);
        public static LambdaMessage GetGamesMessage { get; } = new(LambdaMessageType.GetGames, string.Empty);
        public static LambdaMessage GetHostMessage { get; } = new(LambdaMessageType.GetHost, string.Empty);

        public string PayloadJSON { get; set; } = payloadJson;
        public int ProtocolVersion { get; set; } = PROTOCOL_VERSION;
        public string SourceIdentifier { get; set; } = sourceIdentifier.IsNull() ? Program.HostInformation.Identifier : sourceIdentifier;
        public LambdaMessageType Type { get; set; } = type;

        public LambdaMessage(LambdaMessageType type, string payloadJson) : this(type, string.Empty, payloadJson) {}
    }
}
