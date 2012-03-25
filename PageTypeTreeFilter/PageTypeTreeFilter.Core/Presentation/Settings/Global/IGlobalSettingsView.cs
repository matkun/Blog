namespace PageTypeTreeFilter.Presentation.Settings.Global
{
    public interface IGlobalSettingsView : IView
    {
        string SaveButtonLabel { get; set; }
        string SaveButtonDescription { get; set; }
        string ResetButtonLabel { get; set; }
        string ResetButtonDescription { get; set; }

        string AllowUserSettingsLabel { get; set; }
        string AllowUserSettingsDescription { get; set; }
        bool AllowUserSettings { get; set; }

        string SelectedGlobalPageTypeIds { get; set; }

        event GlobalSettingsEventHandler SaveGlobalSettings;
        event GlobalSettingsEventHandler ResetGlobalSettings;
    }
}
