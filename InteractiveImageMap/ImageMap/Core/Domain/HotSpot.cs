using System;

namespace EPiServer.ImageMap.Core.Domain
{
    [Serializable]
    public class HotSpot
    {
        public Guid? HotSpotId { get; set; }
        public string Tooltip { get; set; }
        public string ClickTargetValue { get; set; }
        public string ClickTargetDisplay { get; set; }
        public string XPosition { get; set; }
        public string YPosition { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

        public static HotSpot Empty
        {
            get
            {
                return new HotSpot
                {
                    HotSpotId = null,
                    Tooltip = string.Empty,
                    ClickTargetValue = string.Empty,
                    ClickTargetDisplay = string.Empty,
                    XPosition = string.Empty,
                    YPosition = string.Empty,
                    Width = string.Empty,
                    Height = string.Empty
                };
            }
        }
    }
}
