using System;
using EPiServer.Personalization;

namespace Lemonwhale.Core.Presentation.EPiServer.UserProfiles
{
    public static class LemonwhaleCurrentUserProfile
    {
        public static Guid PrivateApiKey
        {
            get { return new Guid(EPiServerProfile.Current[LemonwhaleSettingKeys.PrivateApiKey].ToString()); }
            set { EPiServerProfile.Current[LemonwhaleSettingKeys.PrivateApiKey] = value.ToString(); }
        }
    }
}
