using System;
using ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow;

namespace ImageSlideShow.Core.Extensions
{
    public static class ImageSlideExtensions
    {
        public static void EnsureUniqueId(this ImageSlide slide)
        {
            if (slide == null)
            {
                return;
            }
            if (slide.SlideId.IsNullOrEmpty())
            {
                slide.SlideId = Guid.NewGuid();
            }
        }
    }
}
