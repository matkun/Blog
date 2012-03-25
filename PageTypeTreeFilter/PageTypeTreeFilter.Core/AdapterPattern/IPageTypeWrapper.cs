using EPiServer.DataAbstraction;

namespace PageTypeTreeFilter.AdapterPattern
{
    public interface IPageTypeWrapper
    {
        PageTypeCollection List();
        PageType Load(int pageTypeId);
    }
}
