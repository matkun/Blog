using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PageTypeTreeFilter.Presentation.Settings
{
    public partial class PageTypePicker : PresentedControlBase<PageTypePickerPresenter>, IPageTypePickerView
    {
        public bool Enabled { get; set; }

        public string PageTypePickerDescription { get; set; }
        public string AvailablePageTypesLabel { get; set; }
        public string AvailablePageTypesDescription { get; set; }
        public string SelectedPageTypesLabel { get; set; }
        public string SelectedPageTypesDescription { get; set; }

        public string AddPageTypeToolTip { get; set; }
        public string RemovePageTypeToolTip { get; set; }

        public string SelectedPageTypeIds
        {
            get { return hfSelectedPageTypes.Value; }
            set { hfSelectedPageTypes.Value = value; }
        }

        public IEnumerable<ListItem> AvailablePageTypes
        {
            get { return rptAvailablePageTypes.DataSource as IEnumerable<ListItem>; }
            set
            {
                rptAvailablePageTypes.DataSource = value;
                rptAvailablePageTypes.DataBind();
            }
        }

        public IEnumerable<ListItem> SelectedPageTypes
        {
            get { return rptSelectedPageTypes.DataSource as IEnumerable<ListItem>; }
            set
            {
                rptSelectedPageTypes.DataSource = value;
                rptSelectedPageTypes.DataBind();
            }
        }
    }
}
