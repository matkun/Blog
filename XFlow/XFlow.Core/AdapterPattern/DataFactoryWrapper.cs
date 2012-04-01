using EPiServer;
using EPiServer.Core;

namespace XFlow.Core.AdapterPattern
{
    public class DataFactoryWrapper : IDataFactoryWrapper
    {
        public PageDataCollection FindPagesWithCriteria(PageReference pageLink, PropertyCriteriaCollection criteria)
        {
            return DataFactory.Instance.FindPagesWithCriteria(pageLink, criteria);
        }
    }
}
