namespace Lemonwhale.Core.Presentation.EPiServer.UserProfiles
{
    public interface ILemonwhaleSettingsUIPresenterView : IView
    {
        string PersonalApiKeyLabel { get; set; }
        string PersonalApiKeyDescription { get; set; }
        string PersonalApiKeyValidatorErrorMessage { get; set; }
        string PersonalApiKey { get; set; }

        event LemonwhaleSettingsEventHandler SaveLwSettings;
        event LemonwhaleSettingsEventHandler LoadLwSettings;
    }
}
