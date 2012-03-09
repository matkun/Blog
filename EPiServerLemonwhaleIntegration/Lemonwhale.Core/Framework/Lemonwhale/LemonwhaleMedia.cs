using System;
using System.Collections.Generic;
using System.Linq;

namespace Lemonwhale.Core.Framework.Lemonwhale
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

// Why userid and siteid?

//{
//name: "Wagner bom EM-final Berlin 2011",
//id: "b21d9261-ac60-4f0a-bba9-9fbb7145893f",
//description: "",
//duration: 90928,
//views: 5,
//created_at: "2012-03-07T13:22:23+0100",
//userid: "3a743ee9-947a-4db6-bc7e-138ce6ac6cfa",
//categoryid: "29b38774-ea34-4930-ad4e-830706eada92",
//images: {
//  thumbnail_url: "http://pastdlwcd.lwcdn.com/v-i-b21d9261-ac60-4f0a-bba9-9fbb7145893f-1124x70.jpg",
//  normal_image_url: "http://pastdlwcd.lwcdn.com/v-i-b21d9261-ac60-4f0a-bba9-9fbb7145893f-1.jpg"
//},
//siteid: "45069cd7-e3c6-43f3-8424-6f80ad0ed669",
//published_at: "2012-03-07T13:22:23+0100",
//updated_at: "2012-03-07T13:26:55+0100"
//}
