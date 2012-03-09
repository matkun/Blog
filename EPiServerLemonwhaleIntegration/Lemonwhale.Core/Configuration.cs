using System;

namespace Lemonwhale.Core
{
    public static class Configuration
    {
        public static class Lemonwhale
        {
            public static Guid SiteId
            {
                get { return Properties.Settings.Default.LemonwhaleSiteId; }
            }

            public static Guid ApiKey
            {
                get { return Properties.Settings.Default.LemonwhaleApiKey; }
            }

            public static string ListBaseUrl
            {
                get { return Properties.Settings.Default.LemonwhaleListBaseUrl; }
            }
        }
    }
}
