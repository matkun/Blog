using System;
using EPiServer;
using EPiServer.Configuration;
using EPiServer.Framework.Cache;

namespace SitemapEngine.Core.Infrastructure.Cache
{
	public class CacheManager<T> : ICache<T> where T : class
	{
		private readonly TimeSpan _relativeExpiration;
		private readonly string[] _cacheDependencyKeys;
		private static readonly object TempLock = new object();

		public static T TemporaryStorage { get; set; }

		public CacheManager() : this(Settings.Instance.HttpCacheExpiration) { }

		public CacheManager(TimeSpan relativeExpiration)
		{
			TemporaryStorage = null;

			_cacheDependencyKeys = new[]
			{
				InstanceKey
			};

			InitializeCacheDependency();
			_relativeExpiration = relativeExpiration;
		}

		public void Remove(string key)
		{
			CacheManager.Remove(GetCacheKey(key));
		}

		public T this[string key]
		{
			get
			{
				lock (TempLock)
				{
					if (TemporaryStorage != null)
					{
						return TemporaryStorage;
					}
				}

				return CacheManager.Get(GetCacheKey(key)) as T;
			}
			set
			{
				lock (TempLock)
				{
					TemporaryStorage = this[key];
				}

				Remove(key);
				var evictionPolicy = new CacheEvictionPolicy(_cacheDependencyKeys, _relativeExpiration, CacheTimeoutType.Absolute);
				CacheManager.Insert(GetCacheKey(key), value, evictionPolicy);

				lock (TempLock)
				{
					TemporaryStorage = null;
				}
			}
		}

		public T Get(string key, Func<T> factory)
		{
			var result = this[key];
			if (result != null)
			{
				return result;
			}
			result = factory();
			this[key] = result;
			return result;
		}

		public bool Contains(string key)
		{
			return this[key] != null;
		}

		public void InvalidateCachedObjects()
		{
			InitializeCacheDependency();
		}

		private void InitializeCacheDependency()
		{
			CacheManager.Insert(InstanceKey, DateTime.Now.Ticks, null);
		}

		public string InstanceKey
		{
			get { return typeof(T).FullName; }
		}

		private static string GetCacheKey(string key)
		{
			return typeof(T).FullName + "_" + key;
		}
	}
}
