namespace PageTypeTreeFilter.Framework.DataAccess.UserSettings
{
    public interface IUserSettingsRepository
    {
        bool EnableUserSelectedPageTypes { get; }
        string UserSelectedPageTypeIds { get; }
    }
}
