using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;

namespace PageTypeTreeFilter
{
    public static class PageLocator
    {
        public static IEnumerable<PageData> FindPages(string pageTypeId)
        {
            var criteria = new PropertyCriteriaCollection();
            
            if (FilterStrategy.ShouldFilterOnPageType(pageTypeId))
            {
                criteria.Add(PropertyCriteriaGenerator.ForPageType(pageTypeId));
            }

            return DataFactory.Instance.FindPagesWithCriteria(PageReference.RootPage, criteria);
        }

        public static IEnumerable<int> FindAllDistinctParentsTo(IEnumerable<PageData> selectedPages)
        {
            if (selectedPages == null)
            {
                return Enumerable.Empty<int>();
            }
            var pages = selectedPages.ToList();
            if (!pages.Any())
            {
                return Enumerable.Empty<int>();
            }
            var parents = new List<int>();
            foreach (var page in pages)
            {
                parents.AddRange(GetParentsIdsFor(page));
            }
            return parents.Distinct();
        }

        public static IEnumerable<int> PathsToPagesOfSelected(string pageTypeId)
        {
            if (!FilterStrategy.ShouldFilterOnPageType(pageTypeId))
            {
                return Enumerable.Empty<int>();
            }
            var selectedPages = FindPages(pageTypeId).ToList();
            var parents = FindAllDistinctParentsTo(selectedPages);
            return parents.Union(selectedPages.Select(p => p.PageLink.ID));
        }

        private static IEnumerable<int> GetParentsIdsFor(PageData page)
        {
            return DataFactory.Instance.GetParents(page.PageLink).Select(r => r.ID);
        }
    }
}
