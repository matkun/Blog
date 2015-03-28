using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.Templates.Alloy.LanguageTool.Domain;
using EPiServer.Templates.Alloy.LanguageTool.Resources;
using EPiServer.Templates.Alloy.LanguageTool.Wrappers;

namespace EPiServer.Templates.Alloy.LanguageTool.Framework
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly IContentLanguageWrapper _contentLanguage;
        private readonly HttpContextBase _context;
        private static readonly object StoreLock = new object();
        private DynamicDataStore Store { get { return typeof(Translation).GetStore(); } }

        public TranslationRepository(
            IContentLanguageWrapper contentLanguage,
            HttpContextBase context)
        {
            if (contentLanguage == null) throw new ArgumentNullException("contentLanguage");
            if (context == null) throw new ArgumentNullException("context");
            _contentLanguage = contentLanguage;
            _context = context;
        }
        
        public Translation TranslationFor(string resourceKey, string ietfLanguageTag)
        {
            var conditions = ConditionsFor(resourceKey: resourceKey, ietfLanguageTag: ietfLanguageTag);
            lock (StoreLock)
            {
                var existingBags = Store.FindAsPropertyBag(conditions).ToArray();
                if (!existingBags.Any())
                {
                    return null;
                }
                return existingBags.First().ToObject<Translation>();
            }
        }

        public Translation[] TranslationsFor(string contentTypeName, bool ignoreLanguage = true)
        {
            Dictionary<string, object> condition;
            if(ignoreLanguage)
            {
                condition = ConditionsFor(contentType: contentTypeName);
            }
            else
            {
                var language = _contentLanguage.PreferredCulture.IetfLanguageTag;
                condition = ConditionsFor(ietfLanguageTag: language, contentType: contentTypeName);
            }
            lock (StoreLock)
            {
                var existingBags = Store.FindAsPropertyBag(condition).ToArray();
                if (!existingBags.Any())
                {
                    return new Translation[0];
                }
                return existingBags.Select(b => b.ToObject<Translation>()).ToArray();
            }
        }

        public bool UpdateTranslation(Guid translationId, string text, out Translation updatedTranslation)
        {
            PropertyBag bag;
            lock (StoreLock)
            {
                bag = Store.Load(translationId) as PropertyBag;
                if (bag == null)
                {
                    updatedTranslation = null;
                    return false;
                }
                bag["Text"] = text;
                bag["Updated"] = DateTime.Now;
                bag["UpdatedBy"] = _context.User.Identity.Name;
                Store.Save(bag);
            }

            updatedTranslation = bag.ToObject<Translation>();
            return true;
        }

        private static Dictionary<string, object> ConditionsFor(string resourceKey = null, string ietfLanguageTag = null, string contentType = null)
        {
            var conditions = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(resourceKey))
            {
                conditions.Add("ResourceKey", resourceKey);
            }
            if (!string.IsNullOrWhiteSpace(ietfLanguageTag))
            {
                conditions.Add("IetfLanguageTag", ietfLanguageTag);
            }
            if(!string.IsNullOrWhiteSpace(contentType))
            {
                conditions.Add("ContentType", contentType);
            }
            return conditions;
        }
        
        public void InitializeTranslationsFrom(IInitialTranslations resource)
        {
            lock (StoreLock)
            {
                foreach (var translation in Uninitialized(resource))
                {
                    Store.Save(translation);
                }
            }
        }

        private Translation[] Uninitialized(IInitialTranslations resource)
        {
            var uninitialized = new List<Translation>();
            var language = resource.IetfLanguageTag;
            foreach (var group in resource.Initials)
            {
                var translations = group.Translations.Where(t => !AlreadyInitialized(t.ResourceKey, language));
                uninitialized.AddRange(translations);
            }
            return uninitialized.ToArray();
        }

        private bool AlreadyInitialized(string resourceKey, string ietfLanguageTag)
        {
            var conditions = ConditionsFor(resourceKey: resourceKey, ietfLanguageTag: ietfLanguageTag);
            var existingBags = Store.FindAsPropertyBag(conditions);
            return existingBags.Any();
        }

        public void RemoveOrphansFrom(IInitialTranslations resource)
        {
            lock (StoreLock)
            {
                foreach (var translation in Orphans(resource))
                {
                    Store.Delete(translation);
                }
            }
        }

        private Identity[] Orphans(IInitialTranslations resource)
        {
            var condition = ConditionsFor(ietfLanguageTag: resource.IetfLanguageTag);
            var databaseTranslations = Store.FindAsPropertyBag(condition).Select(b => b.ToObject<Translation>()).ToArray();
            if (!databaseTranslations.Any())
            {
                return new Identity[0];
            }

            var initialTranslations = resource.Initials.SelectMany(i => i.Translations).ToArray();
            var orphans = new List<Identity>();
            foreach (var dt in databaseTranslations)
            {
                if (initialTranslations.Any(it => it.ResourceKey == dt.ResourceKey))
                {
                    continue;
                }
                orphans.Add(dt.Id);
            }
            return orphans.ToArray();
        }
    }
}
