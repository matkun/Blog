using System.Configuration;

namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure.Configuration
{
    public class Configuration : ConfigurationSection
    {
        public static Configuration GetConfiguration()
        {
            var configuration = ConfigurationManager.GetSection(Constants.ConfigSection) as Configuration;
            return configuration ?? new Configuration();
        }

        [ConfigurationProperty("InitializeNewTranslations", IsRequired = false)]
        public bool InitializeNewTranslations
        {
            get { return (bool)this["InitializeNewTranslations"]; }
        }

        [ConfigurationProperty("RemoveOrphanTranslations", IsRequired = false)]
        public bool RemoveOrphanTranslations
        {
            get { return (bool)this["RemoveOrphanTranslations"]; }
        }
    }
}
