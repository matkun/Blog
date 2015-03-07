using System;
using EPiServer.Web;

namespace SitemapEngine.Core.Wrappers
{
	public class SiteDefinitionWrapper : ISiteDefinitionWrapper
	{
		public Uri SiteUrl { get { return SiteDefinition.Current.SiteUrl; } }
		public SiteDefinition CurrentSiteDefinition { get { return SiteDefinition.Current; } }
	}
}
