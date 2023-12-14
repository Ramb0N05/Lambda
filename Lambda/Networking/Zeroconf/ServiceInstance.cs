using Makaretu.Dns;
using SharpRambo.ExtensionsLib;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Message = Makaretu.Dns.Message;

namespace Lambda.Networking.Zeroconf {

    public class ServiceInstance {

        #region Private Fields

        private ushort _epPort;

        #endregion Private Fields

        #region Public Properties

        public Message DnsMessage { get; }
        public DomainName InstanceName { get; }
        public List<IPEndPoint> IPEndPoints { get; }
        public IPEndPoint PrimaryEndPoint { get; private set; }
        public IPEndPoint RemoteEndpoint { get; }

        #endregion Public Properties

        #region Public Constructors

        public ServiceInstance(DomainName instanceName, IPEndPoint remoteEndpoint, Message dnsMessage) {
            DnsMessage = dnsMessage;
            InstanceName = instanceName;
            IPEndPoints = getIpEndPoints();
            RemoteEndpoint = remoteEndpoint;
            PrimaryEndPoint = new IPEndPoint(IPAddress.Any, _epPort);
        }

        #endregion Public Constructors

        #region Public Methods

        public void DetectPrimaryEndPoint(bool useIPv4) {
            AddressFamily addressFamily = useIPv4 ? AddressFamily.InterNetwork : AddressFamily.InterNetworkV6;
            UnicastIPAddressInformation? local = getLocalIP(addressFamily);
            IPEndPoint primary = new(IPAddress.Any, _epPort);

            if (local == null && IPEndPoints.Count > 0)
                PrimaryEndPoint = IPEndPoints[0];

            if (local != null && IPEndPoints.Count > 0) {
                foreach (IPEndPoint? ep in IPEndPoints.Where(
                            ep => ep.AddressFamily == addressFamily &&
                            ep.Address.IsInSubnet(local.Address + "/" + local.PrefixLength)
                        )) {
                    primary = ep;
                }
            }

            PrimaryEndPoint = primary;
        }

        #endregion Public Methods

        #region Private Methods

        private List<IPEndPoint> getIpEndPoints() {
            List<IPEndPoint> ip_eps = [];

            if (DnsMessage != null) {
                foreach (ResourceRecord? record in DnsMessage.AdditionalRecords) {
                    if (record != null && record.Class == DnsClass.IN) {
                        if (record.Type == DnsType.SRV && record is SRVRecord srv)
                            _epPort = srv.Port;
                        else if (record.Type is DnsType.A or DnsType.AAAA)
                            ip_eps.Add(new IPEndPoint(new IPAddress(record.GetData()), _epPort));
                    }
                }

                ip_eps = ip_eps.ConvertAll(s => {
                    if (s.Port == 0)
                        s.Port = _epPort;
                    return s;
                });
            }

            return ip_eps;
        }

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

            if (nicIndex >= 0) {
                nic = nics.Length > nicIndex ? nics[nicIndex] : nics[0];
            } else {
                nic = nics.First(n => n.NetworkInterfaceType is NetworkInterfaceType.Ethernet or NetworkInterfaceType.Wireless80211
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

        #endregion Private Methods
    }
}
