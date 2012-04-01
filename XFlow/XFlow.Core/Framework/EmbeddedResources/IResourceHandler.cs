using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace XFlow.Core.Framework.EmbeddedResources
{
    public interface IResourceHandler
    {
        //void AddPageTypePickerStyleSheet(Page page);
        void AddEditCollectionJavaScript(Page page);
        void AddJQueryJson(Page page);
        void AddJavaScript(Page page, string resource);
        void AddStyleSheet(Page page, string resource);
        HtmlLink CreateHtmlLink(string path);
    }
}
