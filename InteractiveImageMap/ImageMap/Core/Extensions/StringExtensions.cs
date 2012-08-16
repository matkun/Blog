using System.Text;

namespace EPiServer.ImageMap.Core.Extensions
{
    public static class StringExtensions
    {
        public static string EscapedJson(this string original)
        {
            if (string.IsNullOrEmpty(original))
                return string.Empty;

            var sb = new StringBuilder(original);
            return sb.Replace("\\", "\\\\").Replace("\"", "\\\"").ToString();
        }
    }
}
