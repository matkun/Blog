using EPiServer.Personalization;

namespace PageTypeTreeFilter.Presentation.Settings.User
{
    public class UserSettingsEventHandlerArgs
    {
        public UserSettingsEventHandlerArgs(string userName, EPiServerProfile data)
        {
            UserName = userName;
            Data = data;
        }

        public string UserName { get; private set; }
        public EPiServerProfile Data { get; private set; }
    }
}
