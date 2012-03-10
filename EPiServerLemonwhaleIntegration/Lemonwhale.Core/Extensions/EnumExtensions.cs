using System;
using System.Linq;
using Lemonwhale.Core.Framework.CustomAttributes;

namespace Lemonwhale.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string StringValue(this Enum stringValue)
        {
            var type = stringValue.GetType();
            var fieldInfo = type.GetField(stringValue.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attributes != null && attributes.Length > 0 ? attributes.First().StringValue : string.Empty;
        }
    }
}
