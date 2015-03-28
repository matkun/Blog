using System.Globalization;
using EPiServer.Globalization;

namespace EPiServer.Templates.Alloy.LanguageTool.Wrappers
{
    public class ContentLanguageWrapper : IContentLanguageWrapper
    {
        public CultureInfo PreferredCulture
        {
            get { return ContentLanguage.PreferredCulture; }
        }
    }
}
