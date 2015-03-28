using System;
using EPiServer.Templates.Alloy.LanguageTool.Domain;
using EPiServer.Templates.Alloy.LanguageTool.Resources;

namespace EPiServer.Templates.Alloy.LanguageTool.Framework
{
    public interface ITranslationRepository
    {
        Translation TranslationFor(string resourceKey, string ietfLanguageTag);
        Translation[] TranslationsFor(string contentTypeName, bool ignoreLanguage = true);
        bool UpdateTranslation(Guid translationId, string text, out Translation updatedTranslation);
        void InitializeTranslationsFrom(IInitialTranslations resource);
        void RemoveOrphansFrom(IInitialTranslations resource);
    }
}
