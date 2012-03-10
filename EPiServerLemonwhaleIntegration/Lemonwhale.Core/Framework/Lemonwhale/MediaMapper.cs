using System;
using System.Collections.Generic;
using Lemonwhale.Core.Domain.Lemonwhale;
using Lemonwhale.Core.Framework.Logging;
using Newtonsoft.Json.Linq;
using log4net;

namespace Lemonwhale.Core.Framework.Lemonwhale
{
    public class MediaMapper : IMediaMapper
    {
        private readonly ICategoryParser _categoryParser;
        private readonly ILog _log;

        public MediaMapper(ICategoryParser categoryParser)
        {
            if (categoryParser == null) throw new ArgumentNullException("categoryParser");
            _categoryParser = categoryParser;
            _log = Log.For(this);
        }

        public LemonwhaleMedia Map(JObject media)
        {
            if(media == null)
            {
                _log.Info("Tried mapping LemonwhaleMedia, but JObject was null. An empty LemonwhaleMedia object was returned.");
                return LemonwhaleMedia.Empty;
            }

            var mediaImages = GetImagesFor(media);
            return new LemonwhaleMedia
                       {
                           Id = GetIdFor(media),
                           Name = media.Value<string>("name"),
                           Description = media.Value<string>("description"),
                           Duration = media.Value<int>("duration"),
                           Views = media.Value<int>("views"),
                           Created = media.Value<DateTime>("created_at"),
                           Published = media.Value<DateTime>("published_at"),
                           Updated = media.Value<DateTime>("updated_at"),
                           CategoryIds = _categoryParser.ParseCategoryIds(media.Value<string>("categoryid")),
                           ThumbnailImageUrl = mediaImages.Value<string>("thumbnail_url"),
                           ImageUrl = mediaImages.Value<string>("normal_image_url"),
                       };
        }

        private JToken GetImagesFor(IDictionary<string, JToken> media)
        {
            JToken images;
            if (!media.TryGetValue("images", out images))
            {
                _log.Info("Tried mapping LemonwhaleMedia 'images', but there was no such value from JSON. An empty JObject object was returned.");
                return new JObject();
            }
            return images;
        }

        private Guid GetIdFor(IDictionary<string, JToken> media)
        {
            JToken id;
            if(!media.TryGetValue("id", out id))
            {
                _log.Info("Tried mapping LemonwhaleMedia 'id', but there was no such value from JSON. An empty Guid object was returned.");
                return Guid.Empty;
            }
            Guid guid;
            if(!Guid.TryParse(id.ToString(), out guid))
            {
                _log.InfoFormat("Tried parsing Guid object from '{0}', it was not on guid format. An empty Guid object was returned.");
                return Guid.Empty;
            }
            return guid;
        }
    }
}
