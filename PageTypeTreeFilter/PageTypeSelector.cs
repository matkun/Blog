using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using EPiServer.DataAbstraction;

namespace PageTypeTreeFilter
{
    public class PageTypeSelector : DropDownList
    {
        public PageTypeSelector()
        {
            var pageTypes = AllPageTypes().ToList();
            pageTypes.Insert(0, new ListItem
                                    {
                                        Text = "--- Show all pages ---",
                                        Value = "show-all-pages"
                                    });
            base.DataSource = pageTypes;
        }

        private static IEnumerable<ListItem> AllPageTypes()
        {
            return PageType.List()
                .Select(pageType => new ListItem
                {
                    Text = pageType.Name,
                    Value = pageType.ID.ToString(CultureInfo.InvariantCulture)
                });
        }
    }
}
