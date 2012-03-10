using System;
using System.Collections.Generic;

namespace Lemonwhale.Core.DataAccess.Lemonwhale
{
    public interface IQueryCollectionGenerator
    {
        Dictionary<string, string> GenerateQueryCollection(
                                        SortBy? sortBy,
                                        SortOrder? sortOrder,
                                        int? page,
                                        int? pageSize,
                                        DateTime? startDate,
                                        DateTime? stopDate,
                                        IEnumerable<Guid> categories,
                                        string searchQuery );
    }
}
