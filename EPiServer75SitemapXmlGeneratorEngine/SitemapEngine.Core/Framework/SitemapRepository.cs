using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Data.Dynamic;
using SitemapEngine.Core.Domain;
using SitemapEngine.Core.Extensions;
using SitemapEngine.Core.Framework.Strategies;
using SitemapEngine.Core.Infrastructure;
using SitemapEngine.Core.Infrastructure.Cache;

namespace SitemapEngine.Core.Framework
{
    public class SitemapRepository : ISitemapRepository
	{
		private readonly IEnumerable<ISitemapStrategy> _strategies;
		private readonly IEntryConverter _converter;
		private readonly ICache<byte[]> _cache;

		public SitemapRepository(
			IEnumerable<ISitemapStrategy> strategies,
			IEntryConverter converter,
			ICache<byte[]> cache)
		{
			if (strategies == null) throw new ArgumentNullException("strategies");
			if (converter == null) throw new ArgumentNullException("converter");
			if (cache == null) throw new ArgumentNullException("cache");
			_strategies = strategies;
			_converter = converter;
			_cache = cache;
		}

		public byte[] ReadSitemapFor(string language)
		{
            return _cache.Get(KeyFor(language), () => SitemapBytesFor(language));
		}
        
        public void RefreshSitemapCacheFor(string language)
        {
            _cache[KeyFor(language)] = SitemapBytesFor(language);
        }
        
		private byte[] SitemapBytesFor(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                return _converter.EmptyBytes();
            }

			language = language.ToLower();
            var store = typeof(SitemapEntry).GetStore();
            var entries = store.Find<SitemapEntry>("Language", language).ToArray();

			if (!entries.Any())
            {
                return _converter.EmptyBytes();
			}
			return _converter.ToBytes(entries);
		}

		public Dictionary<string, int> RebuildSitemap()
		{
			if (_strategies.IsNullOrEmpty())
			{
                var message = string.Format("No strategies for locating URL:s found. You need to have at least one strategy implementing the '{0}' interface.", typeof(ISitemapStrategy).Name);
				throw new Exception(message);
			}

            var entries = AllEntries();
			Persist(entries);

			return entries
					.GroupBy(e => e.Language)
					.ToDictionary(g => g.Key, g => g.Count());
		}

        private static string KeyFor(string language)
        {
            return string.Concat(Constants.CacheKeyPrefix, language);
        }

		private SitemapEntry[] AllEntries()
		{
			var entries = new List<SitemapEntry>();
			foreach (var strategy in _strategies)
			{
				strategy.ForEach(entries.Add);
			}
            return entries.Where(entry => !entry.IsEmpty).ToArray();
		}

		private static void Persist(SitemapEntry[] entries)
        {
            var store = typeof(SitemapEntry).GetStore();
            store.DeleteAll();
            store.SaveAll(entries);
		}
	}
}
