using Lambda.Generic;
using Makaretu.Dns;
using SharpRambo.ExtensionsLib;

namespace Lambda.Networking.Zeroconf {
    public enum ServiceProtocol {
        TCP, UDP
    }

    public class Zeroconf : IDisposable
    {
        public const string DEFAULT_DOMAIN = "local";
        public const ServiceProtocol DEFAULT_SERVICE_PROTOCOL = ServiceProtocol.TCP;

        public string ServiceDomain { get; }
        public ServiceDiscovery ServiceDiscovery { get; }
        public DomainName ServiceFQDN { get; }
        public DomainName ServiceName { get; }
        public List<ServiceInstance> ServiceInstances { get; } = [];
        public ServiceProfile? ServiceProfile { get; private set; }
        public ServiceProtocol ServiceProtocol { get; }
        public static bool UseIPv4 => Program.ConfigManager?.CurrentGeneralConfig.UseIPv4 ?? false;

        public Zeroconf(string serviceName) : this(serviceName, DEFAULT_DOMAIN, DEFAULT_SERVICE_PROTOCOL) { }
        public Zeroconf(string serviceName, string serviceDomain) : this(serviceName, serviceDomain, DEFAULT_SERVICE_PROTOCOL) { }
        public Zeroconf(string serviceName, string serviceDomain, ServiceProtocol serviceProtocol)
        {
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

        public void AdvertiseService(ushort port) => AdvertiseService(getInstanceName(), port);
        public void AdvertiseService(string instanceName, ushort port)
        {
            if (port == 0)
                throw new ArgumentOutOfRangeException(nameof(port), port, "Port must be greater than zero!");

            if (instanceName.IsNull())
                throw new ArgumentNullException(nameof(instanceName));

            if (ServiceProfile != null)
                UnadvertiseService();

            ServiceProfile = new ServiceProfile(instanceName, ServiceName, port);
            ServiceDiscovery.Announce(ServiceProfile);
            ServiceDiscovery.Advertise(ServiceProfile);
        }

        public void UnadvertiseService()
            => ServiceDiscovery.Unadvertise();

        public void Dispose()
        {
            UnadvertiseService();
            ServiceDiscovery.Dispose();
        }

        private string getInstanceName()
        {
            string instanceName = HostInformation.GetComputerName();

            if (instanceName.IsNull())
            {
                instanceName = HostInformation.GetProcessorId();

                if (instanceName.IsNull())
                    instanceName = Guid.NewGuid().ToString();
            }

            return instanceName;
        }

        private void serviceInstanceDiscovered_EventHandler(object? sender, ServiceInstanceDiscoveryEventArgs e)
        {
            if (e.ServiceInstanceName.IsSubdomainOf(ServiceFQDN)
#if !DEBUG
                && ServiceProfile != null && e.ServiceInstanceName != ServiceProfile.FullyQualifiedName
#endif
                && e.Message.AdditionalRecords.Count > 0
                && !ServiceInstances.Any(s => s.InstanceName == e.ServiceInstanceName))
            {
                ServiceInstance si = new(e.ServiceInstanceName, e.RemoteEndPoint, e.Message);

                si.DetectPrimaryEndPoint(UseIPv4);
                ServiceInstances.Add(si);
            }
        }
    }
}
