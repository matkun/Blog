using EPiServer.Personalization;

namespace Lemonwhale.Core.Presentation.EPiServer.UserProfiles
{
    public class LemonwhaleSettingsEventHandlerArgs
    {
        public LemonwhaleSettingsEventHandlerArgs(string userName, EPiServerProfile data)
        {
            UserName = userName;
            Data = data;
        }

        public string UserName { get; private set; }
        public EPiServerProfile Data { get; private set; }
    }
}
