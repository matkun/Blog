using EPiServer.DataAbstraction;

namespace XFlow.Core.AdapterPattern
{
    public class PageTypeWrapper : IPageTypeWrapper
    {
        public PageTypeCollection List()
        {
            return PageType.List();
        }
    }
}
