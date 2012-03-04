using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;

namespace PageTypeTreeFilter
{
    public static class PropertyCriteriaGenerator
    {
        public static PropertyCriteria ForPageType(string pageTypeId)
        {
            return new PropertyCriteria
                       {
                           Name = "PageTypeID",
                           Condition = CompareCondition.Equal,
                           Type = PropertyDataType.PageType,
                           Value = pageTypeId,
                           Required = true
                       };
        }
    }
}
