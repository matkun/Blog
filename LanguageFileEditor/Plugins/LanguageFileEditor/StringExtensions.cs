using System;

namespace EPiServer.Plugins.LanguageFileEditor
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string str, string end)
        {
            return !str.EndsWith(end) ?
                str :
                str.Substring(0, str.LastIndexOf(end, StringComparison.Ordinal));
        }
    }
}
