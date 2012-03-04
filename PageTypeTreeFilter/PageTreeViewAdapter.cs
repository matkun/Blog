using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Adapters;
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
        private int[] _pagesToShow;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            var currentPageExplorer = Control.FindParentControlOfType<PageExplorer>();
            if (currentPageExplorer == null)
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

            _selectedPageType = ((PageTypeSelector)currentPageExplorer
                .FindControlRecursive("PageTypeSelector"))
                .SelectedValue;

            _pagesToShow = PageLocator
                .PathsToPagesOfSelected(_selectedPageType)
                .ToArray();

            if (PageTreeShouldNotBeFiltered())
            {
                return;
            }

            ((PageDataSource)pageTreeView.DataSource).Filter += PageType_Filter;
            pageTreeView.PageTreeViewItemDataBound += BoldifySelectedPageType_OnItemDataBound;
            pageTreeView.DataBind();
        }

        public void PageType_Filter(object sender, FilterEventArgs e)
        {
            if (_pagesToShow == null || !_pagesToShow.Any())
            {
                e.Pages.RemoveRange(0, e.Pages.Count);
                return;
            }

            var clone = new PageDataCollection(e.Pages);
            foreach (var page in clone.Where(p => !_pagesToShow.Contains(p.PageLink.ID)))
            {
                e.Pages.Remove(page);
            }
        }

        public void BoldifySelectedPageType_OnItemDataBound(object sender, PageTreeViewEventArgs e)
        {
            var page = (PageData)e.Item.DataItem;
            if (!ShouldBeIncludedInTree(page) || !FilterStrategy.MatchesFilter(page, _selectedPageType))
            {
                return;
            }

            var htmlAnchor = e.Item.TemplateContainer.Controls.OfType<HtmlAnchor>().First();
            htmlAnchor.Attributes["class"] = String.Join(" ", htmlAnchor.Attributes["class"], "boldify-me");
        }

        private bool PageTreeShouldNotBeFiltered()
        {
            return !FilterStrategy.ShouldFilterOnPageType(_selectedPageType);
        }

        private bool ShouldBeIncludedInTree(PageData page)
        {
            return _pagesToShow.Contains(page.PageLink.ID);
        }
    }
}
