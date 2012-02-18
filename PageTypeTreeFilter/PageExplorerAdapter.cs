using System;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: System.Web.UI.WebResource("PageTypeTreeFilter.Boldify.css", "text/css")]
namespace PageTypeTreeFilter
{
    public class PageExplorerAdapter : ControlAdapter
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddFilterSelectorControls();
            AddBoldifyStylesheet();
        }

        private void AddFilterSelectorControls()
        {
            var container = new HtmlGenericControl("div");
            container.Attributes.Add("class", "page-type-filter-container");
            container.Controls.Add(new Label { Text = "Filter on Page Type:" });

            var pageTypeSelector = new PageTypeSelector
            {
                ID = "PageTypeSelector",
                CssClass = "ContextMenuDropDown",
                DataValueField = "Value",
                DataTextField = "Text",
                AutoPostBack = true
            };

            container.Controls.Add(pageTypeSelector);
            pageTypeSelector.DataBind();
            Control.Controls.AddAt(0, container);
        }

        private void AddBoldifyStylesheet()
        {
            var cssPath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(PageExplorerAdapter), "PageTypeTreeFilter.Boldify.css");

            var cssLink = new HtmlLink { Href = cssPath };
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("media", "screen");
            Page.Header.Controls.Add(cssLink);
        }
    }
}
