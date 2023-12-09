using Lambda.Generic;
using SharpRambo.ExtensionsLib;
using System.Management;

namespace Lambda.Networking {
    public enum LambdaMessageType {
        GetGames = 10,
        GameInfo = 11,

        GetHost = 20,
        HostInfo = 21,
    }

    public class LambdaMessage(LambdaMessageType type, string sourceIdentifier, string payloadJson) {
        public const int PROTOCOL_VERSION = 1;

        public string PayloadJSON { get; set; } = payloadJson;
        public int ProtocolVersion { get; set; } = PROTOCOL_VERSION;
        public string SourceIdentifier { get; set; } = sourceIdentifier.IsNull() ? Program.HostInformation.Identifier : sourceIdentifier;
        public LambdaMessageType Type { get; set; } = type;

        public LambdaMessage(LambdaMessageType type, string payloadJson) : this(type, string.Empty, payloadJson) {}
    }
}
