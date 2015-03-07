using System;
using EPiServer.Web;

namespace SitemapEngine.Core.Wrappers
{
	public interface ISiteDefinitionWrapper 
	{
		Uri SiteUrl { get; }
		SiteDefinition CurrentSiteDefinition { get; }
	}
}
