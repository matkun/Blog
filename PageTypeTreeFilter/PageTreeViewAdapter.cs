using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Adapters;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.UI.Edit;
using EPiServer.UI.WebControls;
using EPiServer.Web.WebControls;

namespace PageTypeTreeFilter
{
    public class PageTreeViewAdapter : WebControlAdapter
    {
        private string _selectedPageType;
        private string SelectedPageType
        {
            get
            {
                if(_selectedPageType == null)
                {
                    _selectedPageType =
                        ((PageTypeSelector)CurrentPageExplorer.FindControlRecursively("PageTypeSelector"))
                        .SelectedValue;
                }
                return _selectedPageType;
            }
        }

        private PageExplorer _currentPageExplorer;
        private PageExplorer CurrentPageExplorer
        {
            get { return _currentPageExplorer ?? (_currentPageExplorer = Control.FindParentControlOfType<PageExplorer>()); }
        }

        private int[] _pagesToKeep;
        public int[] PagesToKeep { get { return _pagesToKeep ?? (_pagesToKeep = PathsToSelectedPages().ToArray()); } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (CurrentPageExplorer == null)
            {
                // This adapter should not be used unless it is placed inside a PageExplorer control
                // as there would not be any PageType to filter on. I.e. it should not be used on
                // the Admin Mode 'Set Access Rights' page.
                return;
            }

            var pageTreeView = Control as PageTreeView;
            if (pageTreeView == null)
            {
                return;
            }

            ((PageDataSource)pageTreeView.DataSource).Filter +=
                new FilterEventHandler(PageExplorerAdapter_Filter);
            pageTreeView.PageTreeViewItemDataBound +=
                new PageTreeViewEventHandler(BoldifySelectedPageType_OnItemDataBound);
            pageTreeView.DataBind();
        }

        private void PageExplorerAdapter_Filter(object sender, FilterEventArgs e)
        {
            if (FilterShouldNotBeApplied())
            {
                return;
            }
            var clone = new PageDataCollection(e.Pages);
            foreach (var page in clone.Where(p => !PagesToKeep.Contains(p.PageLink.ID)))
            {
                e.Pages.Remove(page);
            }
        }

        public void BoldifySelectedPageType_OnItemDataBound(object sender, PageTreeViewEventArgs e)
        {
            var page = (PageData) e.Item.DataItem;
            if (ShouldNotFilterPages() || !ShouldBeIncludedInTree(page) || !IsOfSelectedPageType(page))
            {
                return;
            }

            var htmlAnchor = e.Item.TemplateContainer.Controls.OfType<HtmlAnchor>().First();
            htmlAnchor.Attributes["class"] =
                String.Join(" ", htmlAnchor.Attributes["class"], "boldify-me");
        }

        private IEnumerable<int> PathsToSelectedPages()
        {
            if (string.IsNullOrEmpty(SelectedPageType) || "show-all-pages".Equals(SelectedPageType))
            {
                return Enumerable.Empty<int>();
            }
            var criteria = new PropertyCriteriaCollection
                                {
                                    new PropertyCriteria
                                        {
                                            Name = "PageTypeID",
                                            Condition = CompareCondition.Equal,
                                            Required = true,
                                            Type = PropertyDataType.PageType,
                                            Value = SelectedPageType
                                        }
                                };
            var pagesOfSelectedType = DataFactory.Instance.FindPagesWithCriteria(PageReference.RootPage, criteria);

            var parents = pagesOfSelectedType.Aggregate(Enumerable.Empty<int>(),
                (current, page) => current.Union(DataFactory.Instance.GetParents(page.PageLink).Select(r => r.ID)))
                .ToList();
            parents.Add(PageReference.RootPage.ID);

            return parents.Union(pagesOfSelectedType.Select(p => p.PageLink.ID));
        }

        private bool IsOfSelectedPageType(PageData page)
        {
            return SelectedPageType
                .Equals(page.PageTypeID.ToString(CultureInfo.InvariantCulture));
        }

        private bool ShouldBeIncludedInTree(PageData page)
        {
            return PagesToKeep.Contains(page.PageLink.ID);
        }

        private bool ShouldNotFilterPages()
        {
            return "show-all-pages".Equals(SelectedPageType);
        }

        private bool FilterShouldNotBeApplied()
        {
            return PagesToKeep == null || !PagesToKeep.Any();
        }
    }
}
