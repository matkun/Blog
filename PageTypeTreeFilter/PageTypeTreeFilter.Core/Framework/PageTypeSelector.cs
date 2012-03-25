using System.Web.UI.WebControls;

namespace PageTypeTreeFilter.Framework
{
    public class PageTypeSelector : DropDownList
    {
        public PageTypeSelector()
        {
            base.DataSource = IoC.IoC.Get<IPageTypeStrategy>().AvailablePageTypes();
        }
    }
}
