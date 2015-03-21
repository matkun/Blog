namespace SitemapEngine.Core.Infrastructure
{
	public static class Constants
	{
		public const string WildcardBinding = "*";
        public const string CacheKeyPrefix = "sitemap.xml_";
        public const string XmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

	    public const int SitemapBatchSize = 49999; // Google has a sitemap.xml limit of maximum 50k URLs (or 10MB file size after un-gzipping).

	    public static class Bundles
        {
	        public const string Default = "default";
	        public const string PageTree = "pagetree";
	        public const string Manual = "manual"; // TODO - This bundle name is intendet to be used in a editor interface where one may add static entries
                                                   // TODO - to the sitemap.xml. Feel free to remove it if your client have no need for this kind of functionality.
        }

        public static class Parameters
        {
            public const string Bundle = "bundle";
            public const string Batch = "batch";
        }
	}
}
