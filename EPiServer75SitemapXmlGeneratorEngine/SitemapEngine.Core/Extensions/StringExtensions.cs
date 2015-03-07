using System;

namespace SitemapEngine.Core.Extensions
{
    public static class StringExtensions
    {
        public static string EnsureProtocol(this string url, string protocol = "http")
        {
            if (string.IsNullOrEmpty(url) || url.ValidateProtocol())
            {
                return url;
            }

            return string.Concat(protocol, Uri.SchemeDelimiter, url);
        }

        public static bool ValidateProtocol(this string url)
        {
            return url.StartsWith("http://") || url.StartsWith("https://");
        }
    }
}
