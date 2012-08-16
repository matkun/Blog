using System;
using System.Collections.Generic;

namespace EPiServer.ImageMap.Core.Domain
{
    [Serializable]
    public class ImageMap
    {
        public ImageMap()
        {
            HotSpots = new List<HotSpot>();
        }

        public Guid? ImageMapId { get; set; }
        public List<HotSpot> HotSpots { get; set; }
        public string ImageUrl { get; set; }
        
        public static ImageMap Empty
        {
            get
            {
                return new ImageMap
                {
                    ImageMapId = null,
                    ImageUrl = string.Empty
                };
            }
        }
    }
}
