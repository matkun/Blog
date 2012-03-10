using System;
using System.Collections.Generic;

namespace Lemonwhale.Core.DataAccess.Lemonwhale
{
    public interface ILemonwhaleUriBuilder
    {
        UriBuilder PublicApiUrl(SortBy? sortBy, SortOrder? sortOrder, int? page, int? pageSize, DateTime? startDate, DateTime? stopDate, IEnumerable<Guid> categories, string searchQuery);
    }
}
