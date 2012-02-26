using System.Web.UI.HtmlControls;
using EPiServer;

[assembly: System.Web.UI.WebResource("EmbeddedEPiServerResources.Styles.EmbeddedStylesheet.css", "text/css")]
[assembly: System.Web.UI.WebResource("EmbeddedEPiServerResources.Styles.Images.Embedded_EPiServer_logo.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("EmbeddedEPiServerResources.Scripts.EmbeddedJavaScript.js", "application/javascript")]
namespace EmbeddedEPiServerResources
{
    public partial class EmbeddedControl : UserControlBase
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            IncludeJavaScript();
            IncludeStylesheet();
            IncludeImage();
        }

        private void IncludeJavaScript()
        {
            var jsPath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(EmbeddedControl), "EmbeddedEPiServerResources.Scripts.EmbeddedJavaScript.js");
            Page.ClientScript.RegisterClientScriptInclude("EmbeddedEPiServerResources.Scripts.EmbeddedJavaScript.js", jsPath);
        }

        private void IncludeStylesheet()
        {
            var cssPath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(EmbeddedControl), "EmbeddedEPiServerResources.Styles.EmbeddedStylesheet.css");

            var cssLink = new HtmlLink { Href = cssPath };
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("media", "screen");
            Page.Header.Controls.Add(cssLink);
        }

        private void IncludeImage()
        {
            var imagePath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(EmbeddedControl), "EmbeddedEPiServerResources.Styles.Images.Embedded_EPiServer_logo.gif");
            EmbeddedImage.ImageUrl = imagePath;
        }
    }
}
