using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using XFlow.Core.AdapterPattern;

namespace XFlow.Core.Framework.Services
{
    public class PageLocatorService : IPageLocatorService
    {
        private readonly IDataFactoryWrapper _dataFactory;
        private readonly IPageTypeWrapper _pageType;

        public PageLocatorService(IDataFactoryWrapper dataFactory, IPageTypeWrapper pageType)
        {
            if (dataFactory == null) throw new ArgumentNullException("dataFactory");
            if (pageType == null) throw new ArgumentNullException("pageType");
            _dataFactory = dataFactory;
            _pageType = pageType;
        }

        public PageDataCollection PagesUsingXForm(string formId)
        {
            var xFlowDefinitions = XFlowDefinitions().Distinct();
            var criteria = new PropertyCriteriaCollection();
            foreach (var definitionName in xFlowDefinitions)
            {
                criteria.Add(new PropertyCriteria
                                 {
                                     Name = definitionName,
                                     Type = PropertyDataType.LongString,
                                     Condition = CompareCondition.Contained,
                                     Value = formId
                                 });
            }
            return _dataFactory.FindPagesWithCriteria(PageReference.RootPage, criteria);
        }

        public IEnumerable<string> XFlowDefinitions()
        {
            return from type in _pageType.List()
                   from definition in type.Definitions.Where(UsesXFlow)
                   select definition.Name;
        }

        private static bool UsesXFlow(PageDefinition definition)
        {
            return (definition.Type.Name.IndexOf("XFlow", StringComparison.Ordinal) != -1);
        }
    }
}
