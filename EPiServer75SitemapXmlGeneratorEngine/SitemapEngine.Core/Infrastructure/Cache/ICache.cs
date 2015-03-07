using System;

namespace SitemapEngine.Core.Infrastructure.Cache
{
	public interface ICache<T>
	{
		void Remove(string key);
		T this[string key] { get; set; }
		T Get(string key, Func<T> factory);
		bool Contains(string key);
		void InvalidateCachedObjects();
	}
}
