using System.Xml.Linq;

namespace LanguageScanner
{
    public static class XElementExtensions
    {
        public static string XPath(this XElement element)
        {
            var tmp = element;
            var path = string.Empty;
            while (tmp != null)
            {
                path = string.Concat("/", tmp.Name, path);
                tmp = tmp.Parent;
            }

            // EPiServer's language files starts with <languages><language>, and we don't want those in our paths.
            var startIndex = "/languages/language".Length;
            return path.Substring(startIndex, path.Length - startIndex);
        }
    }
}
