using Makaretu.Dns;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Message = Makaretu.Dns.Message;

namespace Lambda.Networking.Zeroconf {
    public class ServiceInstance {
        public Message DnsMessage { get; }
        public DomainName InstanceName { get; }
        public List<IPEndPoint> IPEndPoints { get; private set; }
        public IPEndPoint PrimaryEndPoint { get; private set; }
        public IPEndPoint RemoteEndpoint { get; }

        private ushort epPort = 0;

        public ServiceInstance(DomainName instanceName, IPEndPoint remoteEndpoint, Message dnsMessage) {
            DnsMessage = dnsMessage;
            InstanceName = instanceName;
            IPEndPoints = getIpEndPoints();
            RemoteEndpoint = remoteEndpoint;
        }

        private List<IPEndPoint> getIpEndPoints() {
            List<IPEndPoint> ip_eps = [];

            if (DnsMessage != null) {
                foreach (ResourceRecord? record in DnsMessage.AdditionalRecords) {
                    if (record != null && record.Class == DnsClass.IN) {
                        if (record.Type == DnsType.SRV && record is SRVRecord srv)
                            epPort = srv.Port;
                        else if (record.Type is DnsType.A or DnsType.AAAA)
                            ip_eps.Add(new IPEndPoint(new IPAddress(record.GetData()), epPort));
                    }
                }

                ip_eps = ip_eps.ConvertAll(s => {
                    if (s.Port == 0)
                        s.Port = epPort;
                    return s;
                });
            }

            return ip_eps;
        }

        public void DetectPrimaryEndPoint(bool useIPv4) {
            AddressFamily addressFamily = useIPv4 ? AddressFamily.InterNetwork : AddressFamily.InterNetworkV6;
            UnicastIPAddressInformation? local = getLocalIP(addressFamily);
            IPEndPoint primary = new(IPAddress.Any, epPort);

            if (local == null && IPEndPoints.Count > 0)
                PrimaryEndPoint = IPEndPoints[0];

            if (local != null && IPEndPoints.Count > 0) {
                foreach (IPEndPoint ep in IPEndPoints.Where(ep => ep.AddressFamily == addressFamily)) {
                    if (ep.Address.IsInSubnet(local.Address + "/" + local.PrefixLength)) {
                        primary = ep;
                        break;
                    }
                }
            }

            PrimaryEndPoint = primary;
        }

        private UnicastIPAddressInformation? getLocalIP() => getLocalIP(AddressFamily.InterNetwork, 0);
        private UnicastIPAddressInformation? getLocalIP(AddressFamily ipAddressFamily) => getLocalIP(ipAddressFamily, 0);
        private UnicastIPAddressInformation? getLocalIP(AddressFamily ipAddressFamily, int nicIndex) {
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
    }
}
