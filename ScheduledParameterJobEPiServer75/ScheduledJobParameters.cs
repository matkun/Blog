using System.Collections.Generic;
using EPiServer.Data.Dynamic;

namespace ScheduledParameterJobEPiServer75
{
    [EPiServerDataStore(
        StoreName = "ScheduledJobParameters",
        AutomaticallyCreateStore = true,
        AutomaticallyRemapStore = true
    )]
    public class ScheduledJobParameters
    {
        public string PluginId { get; set; }
        public Dictionary<string, object> PersistedValues { get; set; }
    }
}
