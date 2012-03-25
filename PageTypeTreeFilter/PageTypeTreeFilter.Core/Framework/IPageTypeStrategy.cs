using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PageTypeTreeFilter.Framework
{
    public interface IPageTypeStrategy
    {
        IEnumerable<ListItem> AvailablePageTypes();
    }
}
