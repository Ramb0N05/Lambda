using SharpRambo.ExtensionsLib;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Lambda.Generic {
    public class HostInformation {
        public string? Description { get; set; }
        public string Identifier { get; }
        public string Name { get; }
        public Version SoftwareVersion { get; }

        public HostInformation() : this(string.Empty, null) { }
        public HostInformation(string? description) : this(string.Empty, description) { }
        public HostInformation(string name, string? description) {
            Description = description;
            Identifier = CalculateIdentifier();
            Name = !name.IsNull() ? name : GetComputerName();
            SoftwareVersion = GetSoftwareVersion();
        }

        public static string CalculateIdentifier()
            => Hashing.ComputeSha256Hash(GetProcessorId() + GetBaseBoardSerial() + GetPhysicalMemorySerial());

        public static string GetBaseBoardSerial() {
            string mbSerial = string.Empty;
            ManagementObjectSearcher mbSearcher = new("select SerialNumber from Win32_BaseBoard");

            foreach (ManagementObject mbObj in mbSearcher.Get().Cast<ManagementObject>())
                mbSerial += mbObj.GetPropertyValue("SerialNumber")?.ToString() ?? string.Empty;

            return mbSerial;
        }

        public static string GetComputerName() {
            string computerName = string.Empty;
            ManagementObjectSearcher computerSearcher = new("select Name from Win32_ComputerSystem");

            foreach (ManagementObject computerObj in computerSearcher.Get().Cast<ManagementObject>())
                computerName = computerObj.GetPropertyValue("Name")?.ToString() ?? string.Empty;

            return computerName;
        }

        public static UnicastIPAddressInformation? GetLocalIP(AddressFamily ipAddressFamily, int nicIndex) {
            NetworkInterface[] nics = [.. NetworkInterface.GetAllNetworkInterfaces().Where(
                nic => nic.OperationalStatus == OperationalStatus.Up
                    && !nic.Name.Contains("vmnet", StringComparison.CurrentCultureIgnoreCase)
                    && !nic.Name.Contains("vethernet", StringComparison.CurrentCultureIgnoreCase)
                    && !nic.Description.Contains("virtual", StringComparison.CurrentCultureIgnoreCase)
            )];

            NetworkInterface nic;

            if (nics.Length == 0)
                return null;

            nic = nicIndex >= 0
                ? nics.Length > nicIndex ? nics[nicIndex] : nics[0]
                : nics.First(n => n.NetworkInterfaceType is NetworkInterfaceType.Ethernet or NetworkInterfaceType.Wireless80211
                                && n.OperationalStatus == OperationalStatus.Up);

            if (nic.NetworkInterfaceType is NetworkInterfaceType.Ethernet or NetworkInterfaceType.Wireless80211 && nic.OperationalStatus == OperationalStatus.Up) {
                foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses) {
                    if (ip.Address.AddressFamily == ipAddressFamily) {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            return ip;

                        if (ip.Address.IsIPv6LinkLocal)
                            return ip;
                        else
                            continue;
                    }
                }
            }

            return null;
        }

        public static string GetPhysicalMemorySerial() {
            string ramSerial = string.Empty;
            ManagementObjectSearcher ramSearcher = new("select SerialNumber from Win32_PhysicalMemory");

            foreach (ManagementObject ramObj in ramSearcher.Get().Cast<ManagementObject>())
                ramSerial += ramObj.GetPropertyValue("SerialNumber")?.ToString() ?? string.Empty;

            return ramSerial;
        }

        public static string GetProcessorId() {
            string cpuId = string.Empty;
            ManagementObjectSearcher cpuSearcher = new("select ProcessorId from Win32_Processor");

            foreach (ManagementObject cpuObj in cpuSearcher.Get().Cast<ManagementObject>())
                cpuId += cpuObj.GetPropertyValue("ProcessorId")?.ToString() ?? string.Empty;

            return cpuId;
        }

        public static Version GetSoftwareVersion()
            => new(Application.ProductVersion);
    }
}
