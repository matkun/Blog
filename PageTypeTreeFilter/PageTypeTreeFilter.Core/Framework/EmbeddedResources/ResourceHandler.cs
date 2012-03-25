using System.Web.UI;
using System.Web.UI.HtmlControls;

[assembly: WebResource("PageTypeTreeFilter.Styles.PageTypePicker.css", "text/css")]
[assembly: WebResource("PageTypeTreeFilter.Styles.UserSettings.css", "text/css")]
[assembly: WebResource("PageTypeTreeFilter.Styles.GlobalSettings.css", "text/css")]
[assembly: WebResource("PageTypeTreeFilter.Styles.Boldify.css", "text/css")]
namespace PageTypeTreeFilter.Framework.EmbeddedResources
{
    public class ResourceHandler : IResourceHandler
    {
        public void AddPageTypePickerStyleSheet(Page page)
        {
            AddStyleSheet(page, "PageTypeTreeFilter.Styles.PageTypePicker.css");
        }

        public void AddUserSettingsStyleSheet(Page page)
        {
            AddStyleSheet(page, "PageTypeTreeFilter.Styles.UserSettings.css");
        }

        public void AddGlobalSettingsStyleSheet(Page page)
        {
            AddStyleSheet(page, "PageTypeTreeFilter.Styles.GlobalSettings.css");
        }

        public void AddBoldifyStyleSheet(Page page)
        {
            AddStyleSheet(page, "PageTypeTreeFilter.Styles.Boldify.css");
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
