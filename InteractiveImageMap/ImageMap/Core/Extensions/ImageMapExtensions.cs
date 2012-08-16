using System;

namespace EPiServer.ImageMap.Core.Extensions
{
    public static class ImageMapExtensions
    {
        public static void EnsureUniqueIds(this Domain.ImageMap imageMap)
        {
            if (imageMap == null)
            {
                return;
            }
            if (imageMap.ImageMapId.IsNullOrEmpty())
            {
                imageMap.ImageMapId = Guid.NewGuid();
            }
            if (imageMap.HotSpots == null)
            {
                return;
            }
            foreach (var hotSpot in imageMap.HotSpots)
            {
                hotSpot.EnsureUniqueId();
            }
        }
    }
}
