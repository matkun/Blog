using System;
using EPiServer.Configuration;
using SitemapEngine.Core.Infrastructure.Cache;
using StructureMap.Configuration.DSL;

namespace SitemapEngine.Core.Infrastructure.Bootstrap.Registries
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For(typeof(ICache<>)).Singleton().Use(typeof(CacheManager<>))
                .Child(typeof(TimeSpan)).Is(Settings.Instance.HttpCacheExpiration);
        }
    }
}
