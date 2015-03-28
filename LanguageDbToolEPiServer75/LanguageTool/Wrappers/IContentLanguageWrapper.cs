using System.Globalization;

namespace EPiServer.Templates.Alloy.LanguageTool.Wrappers
{
    public interface IContentLanguageWrapper
    {
        CultureInfo PreferredCulture { get; }
    }
}
