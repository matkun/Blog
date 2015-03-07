using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Security;

namespace SitemapEngine.Core.Wrappers
{
	public class FilerAccessWrapper : IFilterAccessWrapper
	{
		public bool QueryDistinctAccessEdit(PageData page, AccessLevel accessLevel)
		{
			return FilterAccess.QueryDistinctAccessEdit(page, accessLevel);
		}
	}
}
