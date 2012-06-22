using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ImageSlideShow.Core.Framework.EmbeddedResources
{
    public interface IResourceHandler
    {
        void AddNivoSliderStyleSheet(Page page);
        void AddNivoSliderDefaultThemeStyleSheet(Page page);
        void AddEditCollectionJavaScript(Page page);
        void AddjQuery(Page page);
        void AddjQueryJson(Page page);
        void AddjQueryNivoSlider(Page page);
        void AddJavaScript(Page page, string resource);
        void AddStyleSheet(Page page, string resource);
        HtmlLink CreateHtmlLink(string path);
    }
}
