using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace PageTypeTreeFilter.Framework.EmbeddedResources
{
    public interface IResourceHandler
    {
        void AddStyleSheet(Page page, string resource);
        void AddPageTypePickerStyleSheet(Page page);
        void AddUserSettingsStyleSheet(Page page);
        void AddGlobalSettingsStyleSheet(Page page);
        void AddBoldifyStyleSheet(Page page);
        HtmlLink CreateHtmlLink(string path);
    }
}
