using EPiServer.Data.Dynamic;

namespace PageTypeTreeFilter.Framework.DataAccess.GlobalSettings
{
    [EPiServerDataStore(
        StoreName = "PTTF_GlobalSettings",
        AutomaticallyCreateStore = true,
        AutomaticallyRemapStore = true
    )]
    public class GlobalSettings
    {
        public string SettingsId { get; set; }
        public GlobalSettingsDto Settings { get; set; }
    }
}
