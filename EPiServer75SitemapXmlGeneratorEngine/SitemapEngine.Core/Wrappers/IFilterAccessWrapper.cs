using EPiServer.Core;
using EPiServer.Security;

namespace SitemapEngine.Core.Wrappers
{
	public interface IFilterAccessWrapper
	{
		bool QueryDistinctAccessEdit(PageData page, AccessLevel accessLevel);
	}
}
