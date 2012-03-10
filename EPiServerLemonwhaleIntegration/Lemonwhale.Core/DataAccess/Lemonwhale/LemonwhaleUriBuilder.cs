using System;
using System.Collections.Generic;
using System.Web;
using Lemonwhale.Core.Extensions;
using Lemonwhale.Core.Framework.Logging;
using log4net;

namespace Lemonwhale.Core.DataAccess.Lemonwhale
{
    public class LemonwhaleUriBuilder : ILemonwhaleUriBuilder
    {
        private readonly IQueryCollectionGenerator _queryCollectionGenerator;
        private readonly HttpContextBase _contextBase;
        private readonly ILog _log;

        public LemonwhaleUriBuilder(IQueryCollectionGenerator queryCollectionGenerator, HttpContextBase contextBase)
        {
            if (queryCollectionGenerator == null) throw new ArgumentNullException("queryCollectionGenerator");
            if (contextBase == null) throw new ArgumentNullException("contextBase");
            _queryCollectionGenerator = queryCollectionGenerator;
            _contextBase = contextBase;
            _log = Log.For(this);
        }

        public UriBuilder PublicApiUrl(
            SortBy? sortBy = null,
            SortOrder? sortOrder = null,
            int? page = null,
            int? pageSize = null,
            DateTime? startDate = null,
            DateTime? stopDate = null,
            IEnumerable<Guid> categories = null,
            string searchQuery = null)
        {
            var queryCollection = _queryCollectionGenerator.GenerateQueryCollection(sortBy, sortOrder, page, pageSize, startDate, stopDate, categories, searchQuery);
            var uriBuilder = new UriBuilder(string.Format(Configuration.Lemonwhale.ListBaseUrl, Configuration.Lemonwhale.SiteId))
                                 {
                                     Query = queryCollection.ToQueryString()
                                 };
            _log.InfoFormat("User {0} constructed URL to call Lemonwhale API: {1}", _contextBase.User.Identity.Name, uriBuilder);
            return uriBuilder;
        }
    }
}
