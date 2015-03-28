using EPiServer.Templates.Alloy.LanguageTool.Domain;

namespace EPiServer.Templates.Alloy.LanguageTool.Resources
{
    public interface IInitialTranslations
    {
        string IetfLanguageTag { get; }
        TranslationGroup[] Initials { get; }
    }
}
