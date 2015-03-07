using System;
using System.Collections.Generic;
using System.Linq;
using SitemapEngine.Core.Extensions;
using SitemapEngine.Core.Infrastructure;
using SitemapEngine.Core.Wrappers;

namespace SitemapEngine.Core.Framework
{
	public class HostBindingsService : IHostBindingsService
	{
		private readonly ISiteDefinitionWrapper _siteDefinition;

		public HostBindingsService(ISiteDefinitionWrapper siteDefinition)
		{
			if (siteDefinition == null) throw new ArgumentNullException("siteDefinition");
			_siteDefinition = siteDefinition;
		}

        public Uri DefaultHost()
        {
            return _siteDefinition.SiteUrl;
        }

	    public string[] AllIetfLanguageTags()
	    {
            return _siteDefinition.CurrentSiteDefinition.Hosts
                        .Where(h => h.Language != null && !string.IsNullOrEmpty(h.Language.IetfLanguageTag))
                        .Select(h => h.Language.IetfLanguageTag.ToLower())
                        .Distinct()
                        .ToArray();
	    }

        public string IetfLanguageTagFor(Uri uri)
        {
            if (uri == null || !uri.IsAbsoluteUri)
            {
                return null;
            }
            var currentHost = uri.Host.ToLower();
            if (!uri.IsDefaultPort)
            {
                currentHost = string.Format("{0}:{1}", currentHost, uri.Port);
            }
            var host = _siteDefinition.CurrentSiteDefinition.Hosts.FirstOrDefault(h => currentHost == h.Name.ToLower());
            if (host == null || host.Language == null)
            {
                return null;
            }
            return host.Language.IetfLanguageTag;
        }

	    public Dictionary<string, Uri> AllBindings()
	    {
	        var tags = AllIetfLanguageTags();

            var result = new Dictionary<string, Uri>();
            foreach (var tag in tags)
            {
                var host = _siteDefinition.CurrentSiteDefinition.Hosts
                                .FirstOrDefault(h => h.Language.IetfLanguageTag.ToLower() == tag && h.Name != Constants.WildcardBinding);
                if(host == null)
                {
                    result.Add(tag, DefaultHost());
                    continue;
                }
                result.Add(tag, new Uri(host.Name.EnsureProtocol()));
            }
            return result;
        }
    }
}
