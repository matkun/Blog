using System.Collections.Generic;
using EPiServer.Data.Dynamic;

namespace EPiServer.Plugins.LanguageFileEditor
{
    [EPiServerDataStore(
        StoreName = "LFEChangeTrackingStore",
        AutomaticallyCreateStore = true,
        AutomaticallyRemapStore = true)]
    public class ChangeTrackingContainer
    {
        public string Filename { get; set; }
        public Dictionary<string, string> Content { get; set; }
    }
}