using System.Web.UI;
using System.Web.UI.HtmlControls;

[assembly: WebResource("ImageSlideShow.Core.Styles.nivo-slider.css", "text/css")]
[assembly: WebResource("ImageSlideShow.Core.Styles.themes.default.default.css", "text/css")]
[assembly: WebResource("ImageSlideShow.Core.Scripts.ImageSlideShowEditModeControl.js", "application/javascript")]
[assembly: WebResource("ImageSlideShow.Core.Scripts.jquery-1.7.2.min.js", "application/javascript")]
[assembly: WebResource("ImageSlideShow.Core.Scripts.jquery.json-2.3.min.js", "application/javascript")]
[assembly: WebResource("ImageSlideShow.Core.Scripts.jquery.nivo.slider.js", "application/javascript")]
namespace ImageSlideShow.Core.Framework.EmbeddedResources
{
    public class ResourceHandler : IResourceHandler
    {
        public void AddNivoSliderStyleSheet(Page page)
        {
            AddStyleSheet(page, "ImageSlideShow.Core.Styles.nivo-slider.css");
        }

        public void AddNivoSliderDefaultThemeStyleSheet(Page page)
        {
            AddStyleSheet(page, "ImageSlideShow.Core.Styles.themes.default.default.css");
        }

        public void AddEditCollectionJavaScript(Page page)
        {
            AddJavaScript(page, "ImageSlideShow.Core.Scripts.ImageSlideShowEditModeControl.js");
        }

        public void AddjQuery(Page page)
        {
            AddJavaScript(page, "ImageSlideShow.Core.Scripts.jquery-1.7.2.min.js");
        }

        public void AddjQueryJson(Page page)
        {
            AddJavaScript(page, "ImageSlideShow.Core.Scripts.jquery.json-2.3.min.js");
        }

        public void AddjQueryNivoSlider(Page page)
        {
            AddJavaScript(page, "ImageSlideShow.Core.Scripts.jquery.nivo.slider.js");
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
