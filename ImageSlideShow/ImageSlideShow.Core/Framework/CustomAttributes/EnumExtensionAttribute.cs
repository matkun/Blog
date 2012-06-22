using System;

namespace ImageSlideShow.Core.Framework.CustomAttributes
{
    public class EnumExtensionAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public string SystemId { get; set; }
    }
}
