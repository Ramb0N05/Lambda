using Lambda.Generic;
using Newtonsoft.Json;

namespace Lambda.Models {

    [JsonObject(MemberSerialization.OptIn)]
    public class HostInformationModel {

        [JsonProperty("description", Required = Required.AllowNull)]
        public string? Description { get; set; }

        [JsonProperty("identifier", Required = Required.Always)]
        public string Identifier { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("softwareVersion", Required = Required.Always)]
        public Version SoftwareVersion { get; set; }

        #region JSON Constructor

#pragma warning disable CS8618

        public HostInformationModel() {
        }

#pragma warning restore CS8618

        #endregion JSON Constructor

        #region Initializers

        public static HostInformationModel Create(HostInformation hostInfo)
            => new() {
                Description = hostInfo.Description,
                Identifier = hostInfo.Identifier,
                Name = hostInfo.Name,
                SoftwareVersion = hostInfo.SoftwareVersion,
            };

        #endregion Initializers
    }
}
