using System;
using System.Collections.Generic;
using System.Linq;

namespace Lemonwhale.Core.Domain.Lemonwhale
{
    public class LemonwhaleMedia
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int Views { get; set; }
        public DateTime Created { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string ImageUrl { get; set; }

        public static LemonwhaleMedia Empty
        {
            get
            {
                return new LemonwhaleMedia
                           {
                               Id = Guid.Empty,
                               Name = string.Empty,
                               Description = string.Empty,
                               Duration = 0,
                               Views = 0,
                               Created = DateTime.MinValue,
                               Published = DateTime.MinValue,
                               Updated = DateTime.MinValue,
                               CategoryIds = Enumerable.Empty<Guid>(),
                               ThumbnailImageUrl = string.Empty,
                               ImageUrl = string.Empty
                           };
            }
        }
    }
}
