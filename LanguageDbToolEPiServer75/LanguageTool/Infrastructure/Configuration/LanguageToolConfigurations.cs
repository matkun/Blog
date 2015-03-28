namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure.Configuration
{
    public class LanguageToolConfigurations : ILanguageToolConfigurations
    {
        public bool InitializeNewTranslations { get { return Configuration.GetConfiguration().InitializeNewTranslations; } }
        public bool RemoveOrphanTranslations { get { return Configuration.GetConfiguration().RemoveOrphanTranslations; } }
    }
}
