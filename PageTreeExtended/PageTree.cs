using EPiServer.Core;
using EPiServer.Web.WebControls;

namespace PageTreeExtended
{
    public class PageTree : EPiServer.Web.WebControls.PageTree
    {
        public event PageTreeEventHandler ItemDataBound;

        protected override void CreateChildControls()
        {
            PageData page = PageReference.IsNullOrEmpty(base.RootLink) ? null : base.GetPage(base.RootLink);
            base.CreateTemplateControl(page, this.HeaderTemplate, 0, false);
            this.CreateItemsRecursive(1, string.Empty);
            base.CreateTemplateControl(page, this.FooterTemplate, 0, false);
        }

        private void CreateItemsRecursive(int level, string path)
        {
            PageHierarchicalEnumerable enumerable = base.PageLoader.HierarchicalSelect(path);
            if ((enumerable.Pages.Count != 0) && !PageReference.IsNullOrEmpty(base.RootLink))
            {
                PageHierarchyData data = (path != string.Empty) ? ((PageHierarchyData)enumerable.GetParent()) : new PageHierarchyData(base.PageSource.GetPage(base.RootLink), base.PageLoader);
                base.CreateTemplateControl(data.Page, this.IndentTemplate, level, data.HasChildren);
                foreach (PageHierarchyData data2 in enumerable)
                {
                    PageData page = data2.Page;
                    base.CreateTemplateControl(page, this.ItemHeaderTemplate, data2.Indent, data2.HasChildren);
                    this.CreateItemTemplateControl(page, level, data2.HasChildren, (base.CurrentPage == null) ? PageReference.EmptyReference : base.CurrentPage.PageLink);
                    if ((data2.HasChildren && ((base.NumberOfLevels == -1) || (level < base.NumberOfLevels))) && this.IsExpanded(data2.Page.PageLink))
                    {
                        this.CreateItemsRecursive(level + 1, page.PageLink.ToString());
                    }
                    base.CreateTemplateControl(page, this.ItemFooterTemplate, level, data2.HasChildren);
                }
                base.CreateTemplateControl(data.Page, this.UnindentTemplate, level, data.HasChildren);
            }

        }

        private void CreateItemTemplateControl(PageData page, int level, bool hasChildren, PageReference currentPageLink)
        {
            PageTemplateContainer template = new PageTemplateContainer(page, level, hasChildren);
            if (level == 1)
            {
                if (page.PageLink.CompareToIgnoreWorkID(currentPageLink) && this.IsExpanded(page.PageLink))
                {
                    this.InstantiateSelectedExpandedTopTemplate(template);
                }
                else if (this.IsExpanded(page.PageLink))
                {
                    this.InstantiateExpandedTopTemplate(template);
                }
                else if (page.PageLink.CompareToIgnoreWorkID(currentPageLink))
                {
                    this.InstantiateSelectedTopTemplate(template);
                }
                else
                {
                    this.InstantiateTopTemplate(template);
                }
            }
            else if (page.PageLink.CompareToIgnoreWorkID(currentPageLink) && this.IsExpanded(page.PageLink))
            {
                this.InstantiateSelectedExpandedItemTemplate(template);
            }
            else if (this.IsExpanded(page.PageLink))
            {
                this.InstantiateExpandedItemTemplate(template);
            }
            else if (page.PageLink.CompareToIgnoreWorkID(currentPageLink))
            {
                this.InstantiateSelectedItemTemplate(template);
            }
            else
            {
                this.InstantiateItemTemplate(template);
            }
            if (ItemDataBound != null)
            {
                var pageTreeEventArgs = new PageTreeEventArgs
                {
                    Item = template,
                    DataItem = page
                };
                ItemDataBound.Invoke(this, pageTreeEventArgs);
            }
            this.Controls.Add(template);
        }
    }
}