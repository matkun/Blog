using System;
using System.Collections.Generic;
using System.Linq;
using Lemonwhale.Core.Framework.Logging;
using log4net;

namespace Lemonwhale.Core.Framework.Lemonwhale
{
    public class CategoryParser : ICategoryParser
    {
        private readonly ILog _log;

        public CategoryParser()
        {
            _log = Log.For(this);
        }

        public IEnumerable<Guid> ParseCategoryIds(string categoryIds)
        {
            if (string.IsNullOrEmpty(categoryIds))
            {
                return Enumerable.Empty<Guid>();
            }
            var guids = new List<Guid>();
            foreach (var categoryId in categoryIds.Split(','))
            {
                Guid id;
                if (!Guid.TryParse(categoryId.Trim(), out id))
                {
                    _log.WarnFormat("Failed parsing category id '{0}'; not a valid Guid object.", categoryId);
                    continue;
                }
                guids.Add(id);
            }
            return guids.Distinct();
        }
    }
}
