using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using EPiServer.Data.Dynamic;

namespace ScheduledParameterJobEPiServer75.Extensions
{
    public static class DynamicDataStoreExtensions
    {
        public static void PersistValuesFor(this DynamicDataStore store, string pluginId, IEnumerable<Control> controls, Func<Control, object> controlvalue)
        {
            store.Save(
                new ScheduledJobParameters
                {
                    PluginId = pluginId,
                    PersistedValues = controls
                        .ToDictionary(c => c.ID, controlvalue)
                });
        }

        public static Dictionary<string, object> LoadPersistedValuesFor(this DynamicDataStore store, string pluginId)
        {
            var parameters = store.LoadAll<ScheduledJobParameters>().SingleOrDefault(p => p.PluginId == pluginId);
            return parameters != null ? parameters.PersistedValues : new Dictionary<string, object>();
        }

        public static void RemovePersistedValuesFor(this DynamicDataStore store, string pluginId)
        {
            var existingBags = store.FindAsPropertyBag("PluginId", pluginId);
            foreach (var existingBag in existingBags)
            {
                store.Delete(existingBag);
            }
        }
    }
}
