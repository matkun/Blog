using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PageTypeTreeFilter.Presentation.Settings
{
    public interface IPageTypePickerView : IView
    {
        string PageTypePickerDescription { get; set; }

        string AvailablePageTypesLabel { get; set; }
        string AvailablePageTypesDescription { get; set; }
        string SelectedPageTypesLabel { get; set; }
        string SelectedPageTypesDescription { get; set; }

        string AddPageTypeToolTip { get; set; }
        string RemovePageTypeToolTip { get; set; }

        string SelectedPageTypeIds { get; set; }

        IEnumerable<ListItem> AvailablePageTypes { get; set; }
        IEnumerable<ListItem> SelectedPageTypes { get; set; }
    }
}
