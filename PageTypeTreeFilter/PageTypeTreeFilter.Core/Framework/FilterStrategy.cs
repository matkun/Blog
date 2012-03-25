using System.Globalization;
using EPiServer.Core;

namespace PageTypeTreeFilter.Framework
{
    public static class FilterStrategy
    {
        public static bool ShouldFilterOnPageType(string selectedPageTypeId)
        {
            return !string.IsNullOrEmpty(selectedPageTypeId) && !PageFilterConstants.ShowAllPageTypes.Equals(selectedPageTypeId);
        }
        
        public static bool MatchesFilter(PageData page, string selectedPageTypeId)
        {
            return !ShouldFilterOnPageType(selectedPageTypeId) ||
                selectedPageTypeId.Equals(page.PageTypeID.ToString(CultureInfo.InvariantCulture));
        }
    }
}
