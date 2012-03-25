using EPiServer.DataAbstraction;

namespace PageTypeTreeFilter.AdapterPattern
{
    public class PageTypeWrapper : IPageTypeWrapper
    {
        public PageTypeCollection List()
        {
            return PageType.List();
        }

        public PageType Load(int id)
        {
            return PageType.Load(id);
        }
    }
}
