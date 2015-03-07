using EPiServer.Data.Dynamic;

namespace SitemapEngine.Core.Extensions
{
    public static class DynamicDataStoreExtensions
    {
        public static void SaveAll<T>(this DynamicDataStore store, T[] entries) where T : class
        {
            if(entries == null)
            {
                return;
            }
            foreach (var entry in entries)
            {
                store.Save(entry);
            }
        }
    }
}
