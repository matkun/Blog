using System;
using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.Data.Dynamic;
using PageTypeTreeFilter.Framework.Logging;
using log4net;

namespace PageTypeTreeFilter.Framework.DataAccess.GlobalSettings
{
    public class GlobalSettingsRepository : IGlobalSettingsRepository
    {
        private readonly HttpContextBase _context;
        private readonly ILog _log;

        public GlobalSettingsRepository(HttpContextBase context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _log = Log.For(this);
        }

        public void SaveGlobalSettings(GlobalSettingsDto settings)
        {
            DeleteExistingGlobalSettings();
            Store.Save(new GlobalSettings {SettingsId = SiteSpecificSettingsId, Settings = settings});
            _log.InfoFormat(
                "User '{0}' saved new global settings (AllowUserSettings:'{1}',SelectedPageTypeIds:'{2}') for id '{3}'.",
                _context.User.Identity.Name, settings.AllowUserSettings, settings.SelectedPageTypeIds, SiteSpecificSettingsId);
        }

        public GlobalSettingsDto LoadGlobalSettings()
        {
            var settings = Store.LoadAll<GlobalSettings>().SingleOrDefault(s => s.SettingsId == SiteSpecificSettingsId);
            return settings != null ? settings.Settings : GlobalSettingsDto.Default;
        }

        public void ResetGlobalSettings()
        {
            DeleteExistingGlobalSettings();
            _log.InfoFormat("Global settings reset to default by user '{0}'", _context.User.Identity.Name);
        }

        private static void DeleteExistingGlobalSettings()
        {
            var existingBags = Store.FindAsPropertyBag("SettingsId", SiteSpecificSettingsId);
            foreach (var existingBag in existingBags)
            {
                Store.Delete(existingBag);
            }
        }

        private static DynamicDataStore Store
        {
            get { return typeof(GlobalSettings).GetStore(); }
        }

        private static string SiteSpecificSettingsId
        {
            get { return string.Concat("PageTypeTreeFilterGlobalSettings_", HttpUtility.HtmlEncode(UriSupport.SiteUrl)); }
        }
    }
}
