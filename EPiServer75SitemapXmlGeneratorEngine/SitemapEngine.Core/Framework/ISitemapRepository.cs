using System.Collections.Generic;

namespace SitemapEngine.Core.Framework
{
	public interface ISitemapRepository
    {
        byte[] ReadSitemapFor(SitemapSelector selector);
        void RefreshSitemapCacheFor(string language);
        Dictionary<string, int> RebuildSitemap();
	}
}
