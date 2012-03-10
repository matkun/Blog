using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Lemonwhale.Core.Extensions;

namespace Lemonwhale.Core.DataAccess.Lemonwhale
{
    public class QueryCollectionGenerator : IQueryCollectionGenerator
    {
        public Dictionary<string, string> GenerateQueryCollection(
                                                SortBy? sortBy = null,
                                                SortOrder? sortOrder = null,
                                                int? page = null,
                                                int? pageSize = null,
                                                DateTime? startDate = null,
                                                DateTime? stopDate = null,
                                                IEnumerable<Guid> categories = null,
                                                string searchQuery = null)
        {
            var queryCollection = new Dictionary<string, string>();

            if (sortBy.HasValue)
            {
                queryCollection.Add(QueryParameterKeys.SortBy, sortBy.StringValue());
            }
            if (sortOrder.HasValue)
            {
                queryCollection.Add(QueryParameterKeys.SortOrder, sortOrder.StringValue());
            }
            if (page.HasValue)
            {
                queryCollection.Add(QueryParameterKeys.Page, page.Value.ToString(CultureInfo.InvariantCulture));
            }
            if (pageSize.HasValue)
            {
                queryCollection.Add(QueryParameterKeys.PageSize, pageSize.Value.ToString(CultureInfo.InvariantCulture));
            }
            if (startDate.HasValue && stopDate.HasValue)
            {
                queryCollection.Add(QueryParameterKeys.CreatedBetween, string.Format("{0},{1}", FormatDate(startDate), FormatDate(stopDate)));
            }
            if (categories != null)
            {
                queryCollection.Add(QueryParameterKeys.Categories, string.Join(",", categories.Select(c => c.ToString())));
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                queryCollection.Add(QueryParameterKeys.SearchQuery, HttpUtility.UrlEncode(searchQuery));
            }
            return queryCollection;
        }

        private static string FormatDate(DateTime? dateTime)
        {
            return !dateTime.HasValue ? string.Empty : dateTime.Value.ToString("yyyy-MM-dd");
        }
    }
}
