using SitemapEngine.Core.Domain;

namespace SitemapEngine.Core.Framework
{
	public interface IEntryConverter
	{
        byte[] ToBytes(SitemapEntry[] entries);
        byte[] EmptyBytes();
        byte[] IndexBytes(string[] sitemaps);
	}
}
