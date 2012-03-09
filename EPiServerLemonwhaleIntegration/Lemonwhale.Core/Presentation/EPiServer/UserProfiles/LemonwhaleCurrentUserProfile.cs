using System;
using EPiServer.Personalization;

namespace Lemonwhale.Core.Presentation.EPiServer.UserProfiles
{
    public static class LemonwhaleCurrentUserProfile
    {
        public static Guid PrivateApiKey
        {
            get { return new Guid(EPiServerProfile.Current[LemonwhaleSettingKeys.PersonalApiKey].ToString()); }
            set { EPiServerProfile.Current[LemonwhaleSettingKeys.PersonalApiKey] = value.ToString(); }
        }
    }
}
