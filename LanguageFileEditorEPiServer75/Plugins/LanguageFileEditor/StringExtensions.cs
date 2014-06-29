using System;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string text, string end)
        {
            return !text.EndsWith(end) ?
            text :
            text.Substring(0, text.LastIndexOf(end, StringComparison.Ordinal));
        }
    }
}
