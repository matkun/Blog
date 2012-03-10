using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Personalization;
using EPiServer.PlugIn;
using Lemonwhale.Core.Presentation;
using Lemonwhale.Core.Presentation.EPiServer.UserProfiles;

[assembly: System.Web.UI.WebResource("Lemonwhale.Core.Styles.MediaSelector.css", "text/css")]
namespace Lemonwhale.Core.Presentation.EPiServer.Plugin.Edit
{
    //[GuiPlugIn(
    //    DisplayName = "Lemonwhale",
    //    Description = "User specific Lemonwhale settings",
    //    Area = PlugInArea.SidSettingsArea,
    //    Url = "~/LemonwhaleResource/Lemonwhale.Core.dll/Lemonwhale.Core.Presentation.EPiServer.Plugin.Edit.LemonwhaleMediaSelectorEditControl.ascx")]
    public partial class LemonwhaleMediaSelectorEditControl : UserControlBase //PresentedControlBase<LemonwhaleMediaSelectorEditControlPresenter>, ILemonwhaleMediaSelectorEditControlPresenterView//, IUserSettings, ICustomPlugInLoader
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            IncludeStylesheet();
        }
        private void IncludeStylesheet()
        {
            var cssPath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(LemonwhaleMediaSelectorEditControl), "Lemonwhale.Core.Styles.MediaSelector.css");

            var cssLink = new HtmlLink { Href = cssPath };
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("media", "screen");
            Page.Header.Controls.Add(cssLink);
        }

    }

    public interface ILemonwhaleMediaSelectorEditControlPresenterView : IView
    {
    }

    public class LemonwhaleMediaSelectorEditControlPresenter : PresenterBase
    {
    }
}
