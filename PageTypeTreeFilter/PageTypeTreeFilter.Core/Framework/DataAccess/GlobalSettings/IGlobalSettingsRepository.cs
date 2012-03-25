namespace PageTypeTreeFilter.Framework.DataAccess.GlobalSettings
{
    public interface IGlobalSettingsRepository
    {
        void SaveGlobalSettings(GlobalSettingsDto settings);
        GlobalSettingsDto LoadGlobalSettings();
        void ResetGlobalSettings();
    }
}
