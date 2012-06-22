using System;
using ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow;

namespace ImageSlideShow.Core.Extensions
{
    public static class SlideShowExtensions
    {
        public static void EnsureUniqueIds(this SlideShow show)
        {
            if (show == null)
            {
                return;
            }
            if (show.SlideShowId.IsNullOrEmpty())
            {
                show.SlideShowId = Guid.NewGuid();
            }
            if (show.Slides == null)
            {
                return;
            }
            foreach (var slide in show.Slides)
            {
                slide.EnsureUniqueId();
            }
        }
    }
}
