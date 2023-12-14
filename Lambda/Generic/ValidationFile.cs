using Newtonsoft.Json;

namespace Lambda.Generic {

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ValidationFile : IEquatable<ValidationFile> {
        public static ValidationFile Empty { get; } = new();
        public static ValidationFile Error { get; } = new() { Filename = "<ERR>", Hash = "<ERR>" };

        public FileInfo? File { get; }

        [JsonProperty("path", Required = Required.Always)]
        public string Filename { get; set; } = string.Empty;

        [JsonProperty("hash", Required = Required.Always)]
        public string Hash { get; set; } = string.Empty;

        public bool Equals(ValidationFile? obj)
            => obj != null && File == obj.File && Filename == obj.Filename && Hash == obj.Hash;

        public override bool Equals(object? obj)
            => Equals(obj as ValidationFile);

        public override int GetHashCode()
            => new int[File?.GetHashCode() ?? 0.GetHashCode(), Filename.GetHashCode(), Hash.GetHashCode()].GetHashCode();
    }
}
