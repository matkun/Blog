using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer;
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
        private readonly IHostBindingsService _hostBindingsService;
        private readonly ICache<byte[]> _cache;

		public SitemapRepository(
			IEnumerable<ISitemapStrategy> strategies,
			IEntryConverter converter,
            IHostBindingsService hostBindingsService,
			ICache<byte[]> cache)
		{
			if (strategies == null) throw new ArgumentNullException("strategies");
			if (converter == null) throw new ArgumentNullException("converter");
		    if (hostBindingsService == null) throw new ArgumentNullException("hostBindingsService");
		    if (cache == null) throw new ArgumentNullException("cache");
			_strategies = strategies;
			_converter = converter;
		    _hostBindingsService = hostBindingsService;
		    _cache = cache;
		}

        public byte[] ReadSitemapFor(SitemapSelector selector)
		{
            return _cache.Get(KeyFor(selector), () => SitemapBytesFor(selector));
		}
        
        public void RefreshSitemapCacheFor(string language)
        {
            var selectors = AllSelectorsFor(language);
            foreach (var selector in selectors)
            {
                _cache[KeyFor(selector)] = SitemapBytesFor(selector);
            }

            var indexSelector = new SitemapSelector { Language = language, Bundle = null, Batch = 0 };
            _cache[KeyFor(indexSelector)] = SitemapBytesFor(indexSelector);
        }

        private static string KeyFor(SitemapSelector selector)
        {
            return string.Concat(Constants.CacheKeyPrefix, selector.Language, selector.Bundle, selector.Batch);
        }

        private byte[] SitemapBytesFor(SitemapSelector selector)
        {
            if (string.IsNullOrWhiteSpace(selector.Language))
            {
                return _converter.EmptyBytes();
            }
            if (string.IsNullOrWhiteSpace(selector.Bundle))
            {
                return _converter.IndexBytes(AllSitemapUrlsFor(selector.Language).ToArray());
            }

            selector.Language = selector.Language.ToLower();
            var store = typeof(SitemapEntry).GetStore();

            var condition = ConditionsFor(selector.Bundle, selector.Language);
            var existingBags = store.FindAsPropertyBag(condition).ToArray();
            if (!existingBags.Any())
            {
                return _converter.EmptyBytes();
            }
            var entries = existingBags
                            .Select(b => b.ToObject<SitemapEntry>())
                            .OrderBy(e => e.Location.ToString().ToLower()) // Must sort since we always want to skip the same entries when batching.
                            .Skip(selector.Batch * Constants.SitemapBatchSize)
                            .ToArray();
            return _converter.ToBytes(entries);
        }

        private static IEnumerable<SitemapSelector> AllSelectorsFor(string language)
        {
            var store = typeof(SitemapEntry).GetStore();

            var condition = ConditionsFor(ietfLanguageTag: language);
            var existingBags = store.FindAsPropertyBag(condition).ToArray();

            var entryGroups = existingBags
                            .Select(b => b.ToObject<SitemapEntry>())
                            .GroupBy(e => e.Bundle)
                            .ToArray();

            foreach (var bundle in entryGroups)
            {
                if (string.IsNullOrWhiteSpace(bundle.Key))
                {
                    continue;
                }
                var bundleName = bundle.Key.ToLower();
                var batches = (int)Math.Ceiling((decimal)bundle.Count() / Constants.SitemapBatchSize);
                for (var i = 0; i < batches; i++)
                {
                    yield return new SitemapSelector { Language = language, Bundle = bundleName, Batch = i };
                }
            }
        }

        private IEnumerable<string> AllSitemapUrlsFor(string language)
        {
            Uri baseUri;
            baseUri = _hostBindingsService.AllBindings().TryGetValue(language, out baseUri) ?
                            baseUri :
                            _hostBindingsService.DefaultHost();
             
            var urlBuilder = new UrlBuilder(new Uri(baseUri, "/sitemap.xml"));

            foreach (var selector in AllSelectorsFor(language))
            {
                urlBuilder.QueryCollection.Clear();
                urlBuilder.QueryCollection.Add(Constants.Parameters.Bundle, selector.Bundle);
                urlBuilder.QueryCollection.Add(Constants.Parameters.Batch, selector.Batch.ToString(CultureInfo.InvariantCulture));
                yield return urlBuilder.ToString();
            }
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

        private static Dictionary<string, object> ConditionsFor(string bundle = null, string ietfLanguageTag = null)
        {
            var conditions = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(bundle))
            {
                conditions.Add("Bundle", bundle);
            }
            if (!string.IsNullOrWhiteSpace(ietfLanguageTag))
            {
                conditions.Add("Language", ietfLanguageTag);
            }
            return conditions;
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
