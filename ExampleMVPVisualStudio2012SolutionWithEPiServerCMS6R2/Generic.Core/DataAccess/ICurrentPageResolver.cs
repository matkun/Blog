using EPiServer.Core;

namespace Generic.Core.DataAccess
{
    public interface ICurrentPageResolver
    {
        T CurrentPage<T>(IPageSource page) where T : PageData;
    }
}
