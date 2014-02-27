using EPiServer.Core;
using Generic.Core.DataAccess;
using Generic.Core.Framework;

namespace Generic.Core.Extensions
{
    public static class PageSourceExtensions
    {
        public static T CurrentPage<T>(this IPageSource page) where T : PageData
        {
            return IoC.Get<ICurrentPageResolver>().CurrentPage<T>(page);
        }
    }
}
