using Lambda.Generic;
using Makaretu.Dns;
using SharpRambo.ExtensionsLib;

namespace Lambda.Networking.Zeroconf {

    public enum ServiceProtocol {
        TCP, UDP
    }

    public class Zeroconf : IDisposable {

        #region Constants

        public const string DEFAULT_DOMAIN = "local";
        public const ServiceProtocol DEFAULT_SERVICE_PROTOCOL = ServiceProtocol.TCP;

        #endregion Constants

        #region Public Properties

        public static bool UseIPv4 => Program.ConfigManager?.CurrentGeneralConfig.UseIPv4 ?? false;
        public ServiceDiscovery ServiceDiscovery { get; }
        public string ServiceDomain { get; }
        public DomainName ServiceFQDN { get; }
        public List<ServiceInstance> ServiceInstances { get; } = [];
        public DomainName ServiceName { get; }
        public ServiceProfile? ServiceProfile { get; private set; }
        public ServiceProtocol ServiceProtocol { get; }

        #endregion Public Properties

        #region Public Constructors

        public Zeroconf(string serviceName) : this(serviceName, DEFAULT_DOMAIN, DEFAULT_SERVICE_PROTOCOL) {
        }

        public Zeroconf(string serviceName, string serviceDomain) : this(serviceName, serviceDomain, DEFAULT_SERVICE_PROTOCOL) {
        }

        public Zeroconf(string serviceName, string serviceDomain, ServiceProtocol serviceProtocol) {
            if (serviceName.IsNull())
                throw new ArgumentNullException(nameof(serviceName));

            if (serviceDomain.IsNull())
                serviceDomain = DEFAULT_DOMAIN;

            ServiceDomain = serviceDomain;
            ServiceProtocol = serviceProtocol;

            ServiceName = new DomainName("_" + serviceName, "_" + ServiceProtocol.GetName().ToLower());
            ServiceFQDN = ServiceName + "." + ServiceDomain;

            ServiceDiscovery = new();
            ServiceDiscovery.ServiceInstanceDiscovered += serviceInstanceDiscovered_EventHandler;
        }

        #endregion Public Constructors

        #region Public Methods

        public void AdvertiseService(ushort port) => AdvertiseService(getInstanceName(), port);

        public void AdvertiseService(string instanceName, ushort port) {
            if (port == 0)
                throw new ArgumentOutOfRangeException(nameof(port), port, "Port must be greater than zero!");

            if (instanceName.IsNull())
                throw new ArgumentNullException(nameof(instanceName));

            if (ServiceProfile != null)
                UnadvertiseService();

            ServiceProfile = new ServiceProfile(instanceName, ServiceName, port, MulticastService.GetLinkLocalAddresses());
            ServiceDiscovery.Announce(ServiceProfile);
            ServiceDiscovery.Advertise(ServiceProfile);
        }

        public void UnadvertiseService()
            => ServiceDiscovery.Unadvertise();

        #endregion Public Methods

        #region Private Methods

        private static string getInstanceName() {
            string instanceName = HostInformation.GetComputerName();

            if (instanceName.IsNull()) {
                instanceName = HostInformation.GetProcessorId();

                if (instanceName.IsNull())
                    instanceName = Guid.NewGuid().ToString();
            }

            return instanceName;
        }

        private void serviceInstanceDiscovered_EventHandler(object? sender, ServiceInstanceDiscoveryEventArgs e) {
            if (e.ServiceInstanceName.IsSubdomainOf(ServiceFQDN)
#if !DEBUG
                && ServiceProfile != null && e.ServiceInstanceName != ServiceProfile.FullyQualifiedName
#endif
                && e.Message.AdditionalRecords.Count > 0
                && !ServiceInstances.Exists(s => s.InstanceName == e.ServiceInstanceName)) {
                ServiceInstance si = new(e.ServiceInstanceName, e.RemoteEndPoint, e.Message);

                si.DetectPrimaryEndPoint(UseIPv4);
                ServiceInstances.Add(si);
            }
        }

        #endregion Private Methods

        #region IDisposable

        ~Zeroconf() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                UnadvertiseService();
                ServiceDiscovery.Dispose();
            }
        }

        #endregion IDisposable
    }
}
