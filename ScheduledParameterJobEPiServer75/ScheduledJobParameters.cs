using System.Collections.Generic;
using EPiServer.Data.Dynamic;

namespace EPiServer.Templates.Alloy.ScheduledParameterJob
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
