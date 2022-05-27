using EPiServer.Events.ServiceModel;
using System.ServiceModel;

namespace RemoteEventsTester
{
    internal class RemoteEventProxy : ClientBase<IEventReplication>
    {
        public IEventReplication Interface => Channel;

        public RemoteEventProxy(string endpointConfigurationName) : base(endpointConfigurationName) { }
    }
}
