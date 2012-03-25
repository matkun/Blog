using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using EPiServer.DataAbstraction;

namespace PageTypeTreeFilter.Extensions
{
    public static class PageTypeCollectionExtensions
    {
        public static IEnumerable<ListItem> ToListItems(this PageTypeCollection collection)
        {
            if (collection == null || !collection.Any())
            {
                return Enumerable.Empty<ListItem>();
            }
            return collection.Select(pageType => pageType.ToListItem());
        }
    }
}
