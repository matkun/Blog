using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Lemonwhale.Core.Framework.Lemonwhale
{
    public class MediaMapper : IMediaMapper
    {
        public LemonwhaleMedia Map(JObject media)
        {
            if(media == null)
            {
                return LemonwhaleMedia.Empty;
            }

            var images = (JObject)media["images"];

            return new LemonwhaleMedia
                       {
                           Id = new Guid((string) media["id"]),
                           Name = (string) media["name"],
                           Description = (string) media["description"],
                           Duration = Int32.Parse((string) media["duration"]),
                           Views = Int32.Parse((string) media["views"]),
                           Created = DateTime.Parse((string) media["created_at"]),
                           Published = DateTime.Parse((string) media["published_at"]),
                           Updated = DateTime.Parse((string) media["updated_at"]),
                           CategoryIds = ParseCategoryIds((string)media["categoryid"]),
                           ThumbnailImageUrl = (string) images["thumbnail_url"],
                           ImageUrl = (string) images["normal_image_url"],
                       };
        }

        private static IEnumerable<Guid> ParseCategoryIds(string categoryIds)
        {
            return categoryIds
                .Split(',')
                .Select(id => Guid.Parse(id.Trim()));
        }
    }
}
