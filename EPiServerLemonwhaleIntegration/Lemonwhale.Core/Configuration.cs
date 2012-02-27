using System;

namespace Lemonwhale.Core
{
    public static class Configuration
    {
        public static class Lemonwhale
        {
            public static Guid PrivateApiKey
            {
                get { return Properties.Settings.Default.LemonwhalePrivateAPIKey; }
            }
        }
    }
}
