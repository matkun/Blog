using System;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.Templates.Alloy.LanguageTool.Infrastructure;

namespace EPiServer.Templates.Alloy.LanguageTool.Domain
{
    [EPiServerDataStore(
        StoreName = "TranslationStore",
        AutomaticallyCreateStore = true,
        AutomaticallyRemapStore = true)]
    public class Translation
    {
        public Translation()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
            UpdatedBy = Constants.InitializationName;
        }

        public Identity Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public string ContentType { get; set; }
        public string IetfLanguageTag { get; set; }
        public string ResourceKey { get; set; }
        public string Text { get; set; }
    }
}
