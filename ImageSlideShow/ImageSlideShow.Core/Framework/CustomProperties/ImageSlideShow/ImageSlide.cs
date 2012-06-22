using System;

namespace ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow
{
    [Serializable]
    public class ImageSlide
    {
        public Guid? SlideId { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ImageLinkUrl { get; set; }
        public string ImageTooltip { get; set; }
        
        public static ImageSlide Empty
        {
            get
            {
                return new ImageSlide
                {
                    SlideId = null,
                    ImageUrl = string.Empty,
                    ThumbnailUrl = string.Empty,
                    ImageLinkUrl = string.Empty,
                    ImageTooltip = string.Empty
                };
            }
        }
    }
}
