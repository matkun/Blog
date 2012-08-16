using System;
using EPiServer.ImageMap.Core.Domain;

namespace EPiServer.ImageMap.Core.Extensions
{
    public static class HotSpotExtensions
    {
        public static void EnsureUniqueId(this HotSpot hotSpot)
        {
            if (hotSpot == null)
            {
                return;
            }
            if (hotSpot.HotSpotId.IsNullOrEmpty())
            {
                hotSpot.HotSpotId = Guid.NewGuid();
            }
        }
    }
}
