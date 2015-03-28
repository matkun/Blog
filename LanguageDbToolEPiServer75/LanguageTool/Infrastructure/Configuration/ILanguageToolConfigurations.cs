namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure.Configuration
{
    public interface ILanguageToolConfigurations
    {
        bool InitializeNewTranslations { get; }
        bool RemoveOrphanTranslations { get; }
    }
}
