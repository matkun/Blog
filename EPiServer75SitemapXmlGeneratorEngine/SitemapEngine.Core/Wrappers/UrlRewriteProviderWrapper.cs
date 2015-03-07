using System.Text;
using EPiServer;

namespace SitemapEngine.Core.Wrappers
{
    public class UrlRewriteProviderWrapper : IUrlRewriteProviderWrapper
    {
        public bool GetFriendlyUrl(UrlBuilder url, object internalObject, Encoding toEncoding)
        {
            return Global.UrlRewriteProvider.ConvertToExternal(url, internalObject, toEncoding);
        }
    }
}
