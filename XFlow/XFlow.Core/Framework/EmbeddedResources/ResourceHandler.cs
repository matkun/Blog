using System.Web.UI;
using System.Web.UI.HtmlControls;

//[assembly: WebResource("XFlow.Core.Styles.Foo.css", "text/css")]
[assembly: WebResource("XFlow.Core.Scripts.XFlowEditModeCollectionControl.js", "application/javascript")]
[assembly: WebResource("XFlow.Core.Scripts.jquery.json-2.3.min.js", "application/javascript")]
namespace XFlow.Core.Framework.EmbeddedResources
{
    public class ResourceHandler : IResourceHandler
    {
        //public void AddPageTypePickerStyleSheet(Page page)
        //{
        //    AddStyleSheet(page, "XFlow.Core.Styles.Foo.css");
        //}

        public void AddEditCollectionJavaScript(Page page)
        {
            AddJavaScript(page, "XFlow.Core.Scripts.XFlowEditModeCollectionControl.js");
        }

        public void AddJQueryJson(Page page)
        {
            AddJavaScript(page, "XFlow.Core.Scripts.jquery.json-2.3.min.js");
        }

        public void AddJavaScript(Page page, string resource)
        {
            var jsPath = page
                .ClientScript
                .GetWebResourceUrl(typeof(ResourceHandler), resource);
            page.ClientScript.RegisterClientScriptInclude(resource, jsPath);
        }

       public void AddStyleSheet(Page page, string resource)
        {
            var cssPath = page
                .ClientScript
                .GetWebResourceUrl(typeof(ResourceHandler), resource);
            page.Header.Controls.Add(CreateHtmlLink(cssPath));
        }

        public HtmlLink CreateHtmlLink(string path)
        {
            var cssLink = new HtmlLink { Href = path };
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("media", "screen");
            return cssLink;
        }
    }
}
