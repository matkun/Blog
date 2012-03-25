using System;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PageTypeTreeFilter.Framework;
using PageTypeTreeFilter.Framework.Authorization;
using PageTypeTreeFilter.Framework.EmbeddedResources;
using PageTypeTreeFilter.Framework.IoC;
using PageTypeTreeFilter.Framework.Language;

namespace PageTypeTreeFilter
{
    public class PageExplorerAdapter : ControlAdapter
    {
        private readonly ITranslator _translator;
        private readonly IResourceHandler _resourceHandler;
        private readonly IRoleStrategy _roleStrategy;

        public PageExplorerAdapter()
        {
            _translator = IoC.Get<ITranslator>();
            _resourceHandler = IoC.Get<IResourceHandler>();
            _roleStrategy = IoC.Get<IRoleStrategy>();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddFilterSelectorControls();
            _resourceHandler.AddBoldifyStyleSheet(Page);
        }

        private void AddFilterSelectorControls()
        {
            var container = new HtmlGenericControl("div");
            container.Attributes.Add("class", "page-type-filter-container");
            container.Controls.Add(new Label
                                       {
                                           Text = _translator.Translate("/PageTypeTreeFilter/PageExplorerAdapter/FilterSelectorLabel")
                                       });

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

            if (_roleStrategy.IsAdministrator())
            {
                container.Controls.Add(GlobalSettingsLink());
            }

            Control.Controls.AddAt(0, container);
        }

        private HyperLink GlobalSettingsLink()
        {
            return new HyperLink
                       {
                           Text = _translator.Translate("/PageTypeTreeFilter/PageExplorerAdapter/SettingsLinkText"),
                           ToolTip =_translator.Translate("/PageTypeTreeFilter/PageExplorerAdapter/SettingsLinkTooltip"),
                           NavigateUrl ="/PageTypeTreeFilterResource/PageTypeTreeFilter.dll/PageTypeTreeFilter.Presentation.Settings.Global.GlobalSettings.aspx",
                           Target = "EditPanel"
                       };
        }
    }
}
