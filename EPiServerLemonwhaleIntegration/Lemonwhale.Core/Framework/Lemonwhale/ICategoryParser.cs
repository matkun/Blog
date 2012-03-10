using System;
using System.Collections.Generic;

namespace Lemonwhale.Core.Framework.Lemonwhale
{
    public interface ICategoryParser
    {
        IEnumerable<Guid> ParseCategoryIds(string categoryIds);
    }
}
