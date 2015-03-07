using System.Text;
using EPiServer;

namespace SitemapEngine.Core.Wrappers
{
    public interface IUrlRewriteProviderWrapper
    {
        bool GetFriendlyUrl(UrlBuilder url, object internalObject, Encoding toEncoding);
    }
}
