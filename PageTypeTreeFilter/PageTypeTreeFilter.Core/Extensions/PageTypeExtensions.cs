using System.Globalization;
using System.Web.UI.WebControls;
using EPiServer.DataAbstraction;

namespace PageTypeTreeFilter.Extensions
{
    public static class PageTypeExtensions
    {
        public static ListItem ToListItem(this PageType type)
        {
            if(type == null)
            {
                return null;
            }
            return new ListItem
                       {
                           Text = type.Name,
                           Value = type.ID.ToString(CultureInfo.InvariantCulture)
                       };
        }
    }
}
