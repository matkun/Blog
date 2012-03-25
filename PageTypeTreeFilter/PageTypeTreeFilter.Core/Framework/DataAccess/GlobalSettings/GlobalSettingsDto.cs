using System.Linq;
using EPiServer.DataAbstraction;

namespace PageTypeTreeFilter.Framework.DataAccess.GlobalSettings
{
    public class GlobalSettingsDto
    {
        public bool AllowUserSettings { get; set; }
        public string SelectedPageTypeIds { get; set; }

        public static GlobalSettingsDto Default
        {
            get { return new GlobalSettingsDto { AllowUserSettings = true, SelectedPageTypeIds = DefaultAvailablePageTypes() }; }
        }

        private static string DefaultAvailablePageTypes()
        {
            return string.Join(";", PageType.List().Select(t => t.ID));
        }
    }
}
