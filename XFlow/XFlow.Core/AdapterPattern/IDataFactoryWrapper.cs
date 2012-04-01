using EPiServer;
using EPiServer.Core;

namespace XFlow.Core.AdapterPattern
{
    public interface IDataFactoryWrapper
    {
        PageDataCollection FindPagesWithCriteria(PageReference pageLink, PropertyCriteriaCollection criteria);
    }
}
