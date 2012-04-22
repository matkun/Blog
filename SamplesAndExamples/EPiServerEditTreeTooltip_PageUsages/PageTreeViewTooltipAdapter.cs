using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.Adapters;
using EPiServer.Core;
using EPiServer.UI.WebControls;

namespace EPiServer.CodeSample.EditTreeToolTip
{
    public class PageTreeViewTooltipAdapter : ControlAdapter
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            var treeView = Control as PageTreeView;
            if(treeView == null)
            {
                return;
            }
            treeView.PageTreeViewItemDataBound += AddTooltipHandler;
        }

        private static void AddTooltipHandler(object sender, PageTreeViewEventArgs e)
        {
            var page = e.Item.DataItem as PageData;
            if(page == null)
            {
                return;
            }
            var ids = string.Join(", ", FindUsagesFor(page.PageLink));
            e.Item.Attributes.Add("UsedOn", ids);
        }

        private static IEnumerable<string> FindUsagesFor(PageReference pageReference)
        {
            var references = DataFactory.Instance.GetLinksToPages(pageReference);
            if(references == null || !references.Any())
            {
                return new[] {"-"};
            }
            return references.Select(reference => reference.ID.ToString(CultureInfo.InvariantCulture));
        }
    }
}
