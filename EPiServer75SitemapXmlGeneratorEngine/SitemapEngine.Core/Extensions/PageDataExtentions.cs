using System;
using System.Linq;
using System.Text;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using SitemapEngine.Core.CustomAttributes;
using SitemapEngine.Core.Domain;
using SitemapEngine.Core.Framework;
using SitemapEngine.Core.Wrappers;

namespace SitemapEngine.Core.Extensions
{
	public static class PageDataExtentions
	{
		public static bool IsIndexablePageType(this PageData page)
		{
			return !page
					.GetType()
					.GetCustomAttributes(typeof(ExcludeFromSitemapAttribute), true)
					.Any();
		}

		public static SitemapEntry ToSitemapEntry(this PageData page, LanguageSelector language)
		{
			if (page == null)
			{
				return SitemapEntry.Empty;
			}
            
			var urlRewriter = ServiceLocator.Current.GetInstance<IUrlRewriteProviderWrapper>();
		    var hostBindingsService = ServiceLocator.Current.GetInstance<IHostBindingsService>();
		   
		    Uri baseUri;
            baseUri = hostBindingsService.AllBindings().TryGetValue(language.LanguageBranch, out baseUri) ?
                            baseUri :
                            hostBindingsService.DefaultHost();
			var location = new UrlBuilder(new Uri(baseUri, page.LinkURL.ToLower()));
			urlRewriter.GetFriendlyUrl(location, null, Encoding.UTF8);

			return new SitemapEntry
			{
				IsEmpty = false,
				Location = location.Uri,
                LastModified = page.Changed,
				ChangeFrequency = Frequency.Weekly,
				Priority = 0.5F,
				Language = language.LanguageBranch
			};
		}
	}
}
