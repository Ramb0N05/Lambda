using SharpRambo.ExtensionsLib;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Lambda.Generic {

    public class HostInformation(string name, string? description) {
        public string? Description { get; set; } = description;
        public string Identifier { get; } = CalculateIdentifier();
        public string Name { get; } = !name.IsNull() ? name : GetComputerName();
        public Version SoftwareVersion { get; } = GetSoftwareVersion();

        public HostInformation() : this(string.Empty, null) {
        }

        public HostInformation(string? description) : this(string.Empty, description) {
        }

        public static string CalculateIdentifier()
            => Hashing.ComputeSha256Hash(GetProcessorId() + GetBaseBoardSerial() + GetPhysicalMemorySerial());

        public static string GetBaseBoardSerial() {
            StringBuilder mbSerial = new();
            ManagementObjectSearcher mbSearcher = new("select SerialNumber from Win32_BaseBoard");

            foreach (ManagementObject mbObj in mbSearcher.Get().Cast<ManagementObject>())
                mbSerial.Append(mbObj.GetPropertyValue("SerialNumber")?.ToString() ?? string.Empty);

            return mbSerial.ToString();
        }

        public static string GetComputerName() {
            string computerName = string.Empty;
            ManagementObjectSearcher computerSearcher = new("select Name from Win32_ComputerSystem");

            foreach (ManagementObject computerObj in computerSearcher.Get().Cast<ManagementObject>())
                computerName = computerObj.GetPropertyValue("Name")?.ToString() ?? string.Empty;

            return computerName;
        }

        public static UnicastIPAddressInformation? GetLocalIP() => GetLocalIP(AddressFamily.InterNetwork, 0);

        public static UnicastIPAddressInformation? GetLocalIP(AddressFamily ipAddressFamily) => GetLocalIP(ipAddressFamily, 0);

        public static UnicastIPAddressInformation? GetLocalIP(AddressFamily ipAddressFamily, int interfaceIndex) {
            NetworkInterface[] nics = [.. NetworkInterface.GetAllNetworkInterfaces().Where(
                nic => nic.OperationalStatus == OperationalStatus.Up
                    && !nic.Name.Contains("vmnet", StringComparison.CurrentCultureIgnoreCase)
                    && !nic.Name.Contains("vethernet", StringComparison.CurrentCultureIgnoreCase)
                    && !nic.Description.Contains("virtual", StringComparison.CurrentCultureIgnoreCase)
            )];

            NetworkInterface nic;

            if (nics.Length == 0)
                return null;

            if (interfaceIndex >= 0) {
                nic = nics.Length > interfaceIndex ? nics[interfaceIndex] : nics[0];
            } else {
                nic = nics.First(n =>
                    n.NetworkInterfaceType is NetworkInterfaceType.Ethernet or NetworkInterfaceType.Wireless80211
                    && n.OperationalStatus == OperationalStatus.Up);
            }

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
            StringBuilder ramSerial = new();
            ManagementObjectSearcher ramSearcher = new("select SerialNumber from Win32_PhysicalMemory");

            foreach (ManagementObject ramObj in ramSearcher.Get().Cast<ManagementObject>())
                ramSerial.Append(ramObj.GetPropertyValue("SerialNumber")?.ToString() ?? string.Empty);

            return ramSerial.ToString();
        }

        public static string GetProcessorId() {
            StringBuilder cpuId = new();
            ManagementObjectSearcher cpuSearcher = new("select ProcessorId from Win32_Processor");

            foreach (ManagementObject cpuObj in cpuSearcher.Get().Cast<ManagementObject>())
                cpuId.Append(cpuObj.GetPropertyValue("ProcessorId")?.ToString() ?? string.Empty);

            return cpuId.ToString();
        }

        public static Version GetSoftwareVersion()
            => Assembly.GetExecutingAssembly().GetName().Version ?? Version.Parse("1.0.0.0");
    }
}
