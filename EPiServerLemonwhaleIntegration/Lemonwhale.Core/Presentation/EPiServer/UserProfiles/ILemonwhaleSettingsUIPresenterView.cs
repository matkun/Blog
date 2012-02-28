namespace Lemonwhale.Core.Presentation.EPiServer.UserProfiles
{
    public interface ILemonwhaleSettingsUIPresenterView : IView
    {
        string PrivateApiKeyLabel { get; set; }
        string PrivateApiKey { get; set; }

        event LemonwhaleSettingsEventHandler Save;
        event LemonwhaleSettingsEventHandler Load;
    }
}
