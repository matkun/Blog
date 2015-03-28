using System;
using EPiServer.Templates.Alloy.LanguageTool.Wrappers;

namespace EPiServer.Templates.Alloy.LanguageTool.Framework
{
    public class LocalizationServiceDecorator : ILocalizationServiceWrapper
    {
        private readonly ILocalizationServiceWrapper _inner;
        private readonly IContentLanguageWrapper _contentLanguage;
        private readonly ITranslationRepository _translationRepository;

        public LocalizationServiceDecorator(
            ILocalizationServiceWrapper inner,
            IContentLanguageWrapper contentLanguage,
            ITranslationRepository translationRepository)
        {
            if (inner == null) throw new ArgumentNullException("inner");
            if (contentLanguage == null) throw new ArgumentNullException("contentLanguage");
            if (translationRepository == null) throw new ArgumentNullException("translationRepository");
            _inner = inner;
            _contentLanguage = contentLanguage;
            _translationRepository = translationRepository;
        }

        public string GetString(string resourceKey)
        {
            return FromDatabaseOrDefault(resourceKey) ?? _inner.GetString(resourceKey);
        }

        private string FromDatabaseOrDefault(string resourceKey)
        {
            var ietfLanguageTag = _contentLanguage.PreferredCulture.IetfLanguageTag;
            var translation = _translationRepository.TranslationFor(resourceKey, ietfLanguageTag);
            return translation == null ? null : translation.Text;
        }
    }
}
