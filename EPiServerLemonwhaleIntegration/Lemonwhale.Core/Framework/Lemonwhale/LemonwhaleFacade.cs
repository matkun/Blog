using System;
using System.Collections.Generic;
using System.Linq;
using Lemonwhale.Core.AdapterPattern;
using Lemonwhale.Core.DataAccess.Lemonwhale;
using Lemonwhale.Core.Domain.Lemonwhale;
using Lemonwhale.Core.Extensions;
using Lemonwhale.Core.Framework.Logging;
using Newtonsoft.Json.Linq;
using log4net;

namespace Lemonwhale.Core.Framework.Lemonwhale
{
    public class LemonwhaleFacade : ILemonwhaleFacade
    {
        private readonly IMediaMapper _mediaMapper;
        private readonly ILemonwhaleUriBuilder _lemonwhaleUriBuilder;
        private readonly IWebClientWrapper _webClient;
        private readonly ILog _log;

        public LemonwhaleFacade(IMediaMapper mediaMapper, ILemonwhaleUriBuilder lemonwhaleUriBuilder, IWebClientWrapper webClient)
        {
            if (mediaMapper == null) throw new ArgumentNullException("mediaMapper");
            if (lemonwhaleUriBuilder == null) throw new ArgumentNullException("lemonwhaleUriBuilder");
            if (webClient == null) throw new ArgumentNullException("webClient");
            _mediaMapper = mediaMapper;
            _lemonwhaleUriBuilder = lemonwhaleUriBuilder;
            _webClient = webClient;
            _log = Log.For(this);
        }

        public IEnumerable<LemonwhaleMedia> GetAllPublicMedia()
        {
            var publicApiUrl = PublicApiUrl();
            var apiResponse = _webClient.DownloadString(publicApiUrl);
            if (string.IsNullOrEmpty(apiResponse))
            {
                _log.ErrorFormat("Failed to get JSON from API on URL: {0}", publicApiUrl);
                return Enumerable.Empty<LemonwhaleMedia>();
            }

            var json = JObject.Parse(apiResponse);
            if(json == null)
            {
                _log.ErrorFormat("Failed parsing JObject from API-Response JSON: {0}", apiResponse);
                return Enumerable.Empty<LemonwhaleMedia>();
            }

            var videos = (JArray) json["videos"];
            if(videos.IsNullOrEmpty())
            {
                _log.ErrorFormat("Video collection was null or empty for API-Response JSON: {0}", apiResponse);
                return Enumerable.Empty<LemonwhaleMedia>();
            }

            return videos.Select(video => _mediaMapper.Map(video as JObject));
        }

        // Needed to allow optional filter parameters as interface cannot have them.
        private string PublicApiUrl(SortBy? sortBy = null, SortOrder? sortOrder = null, int? page = null, int? pageSize = null, DateTime? startDate = null,
                                    DateTime? stopDate = null, IEnumerable<Guid> categories = null, string searchQuery = null)
        {
            return _lemonwhaleUriBuilder
                .PublicApiUrl(sortBy: sortBy, sortOrder: sortOrder, page: page, pageSize: pageSize, startDate: startDate,
                              stopDate: stopDate, categories: categories, searchQuery: searchQuery)
                .ToString();
        }
    }

    public interface ILemonwhaleFacade
    {
        IEnumerable<LemonwhaleMedia> GetAllPublicMedia();
        //LemonwhaleMedia GetMediaFor(Guid mediaId);
        //LemonwhaleCategory GetAllCategories();
    }
}
