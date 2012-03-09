using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        //public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string ImageUrl { get; set; }
        //public Guid SiteId { get; set; }
    }
}



//images: {
//thumbnail_url: "http://pae1d8a01.lwcdn.com/v-i-02c26624-4c68-4b48-90f7-4d4cb6722d7f-0124x70.jpg",
//normal_image_url: "http://pae1d8a01.lwcdn.com/v-i-02c26624-4c68-4b48-90f7-4d4cb6722d7f-0.jpg"
//},
//siteid: "b2801424-fef4-4594-8b7d-200466fdc8db",
//}