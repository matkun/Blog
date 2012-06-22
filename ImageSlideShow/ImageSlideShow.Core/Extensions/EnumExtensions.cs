using System;
using ImageSlideShow.Core.Framework.CustomAttributes;

namespace ImageSlideShow.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string DisplayNameOrDefault(this Enum e)
        {
            var extensionAttribute = EnumExtensionAttribute(e);
            return extensionAttribute != null ? extensionAttribute.DisplayName : e.ToString();
        }

        public static string SystemIdOrDefault(this Enum e)
        {
            var extensionAttribute = EnumExtensionAttribute(e);
            return extensionAttribute != null ? extensionAttribute.SystemId : e.ToString();
        }

        private static EnumExtensionAttribute EnumExtensionAttribute(Enum e)
        {
            var type = e.GetType();
            var value = type.GetField(e.ToString());
            var attributes = value.GetCustomAttributes(typeof (EnumExtensionAttribute), false);
            return attributes.Length > 0 ? (EnumExtensionAttribute) attributes[0] : null;
        }
    }
}
