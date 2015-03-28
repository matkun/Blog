using System;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Templates.Alloy.LanguageTool.Domain;

namespace EPiServer.Templates.Alloy.LanguageTool.Extensions
{
    public static class ListExtensions
    {
        public static void SetupAndAdd(this List<TranslationGroup> list, TranslationGroup group, string ietfLanguageTag)
        {
            if(group.Translations == null)
            {
                throw new Exception("Translations cannot be null in added TranslationGroup.");
            }
            if(string.IsNullOrWhiteSpace(ietfLanguageTag))
            {
                throw new Exception("Translation group must have an IETF language tag; such as 'en' or 'sv'.");
            }
            if(group.ContentTypeName == null)
            {
                // ContentData type translations are only editable from global tool in EPiServer edit mode.
                group.ContentTypeName = typeof(ContentData).Name;
            }
            ietfLanguageTag = ietfLanguageTag.ToLower();

            foreach (var translation in group.Translations)
            {
                translation.IetfLanguageTag = ietfLanguageTag;
                translation.ContentType = group.ContentTypeName;
            }
            list.Add(group);
        }
    }
}
