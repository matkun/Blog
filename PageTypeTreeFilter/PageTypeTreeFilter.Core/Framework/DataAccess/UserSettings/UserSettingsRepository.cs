using PageTypeTreeFilter.Presentation.Settings.User;

namespace PageTypeTreeFilter.Framework.DataAccess.UserSettings
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        public bool EnableUserSelectedPageTypes
        {
            get { return (bool) EPiServer.Personalization.EPiServerProfile.Current[UserSettingsKeys.EnableUserSelectedPageTypes]; }
        }

        public string UserSelectedPageTypeIds
        {
            get { return EPiServer.Personalization.EPiServerProfile.Current[UserSettingsKeys.UserSelectedPageTypes] as string ?? string.Empty; }
        }
    }
}
