namespace PageTypeTreeFilter.Presentation.Settings.User
{
    public interface IUserSettingsView : IView
    {
        string EnablePersonalPageTypesLabel { get; set; }
        string EnablePersonalPageTypesDescription { get; set; }
        string UserSettingsDisabledMessage { get; set; }
        bool EnablePersonalPageTypes { get; set; }
        bool AllowUserSettings { get; set; }

        string SelectedPageTypeIds { get; set; }

        event UserSettingsEventHandler SaveUserSettings;
        event UserSettingsEventHandler LoadUserSettings;
    }
}
