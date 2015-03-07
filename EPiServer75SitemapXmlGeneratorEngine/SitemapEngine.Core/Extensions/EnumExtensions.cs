using System;
using SitemapEngine.Core.CustomAttributes;

namespace SitemapEngine.Core.Extensions
{
	public static class EnumExtensions
	{
		public static string ToValueString(this Enum @enum)
		{
			var attributes = (ValueAttribute[])@enum
								.GetType()
								.GetField(@enum.ToString())
								.GetCustomAttributes(typeof(ValueAttribute), false);
			return attributes.Length > 0 ? attributes[0].Value : string.Empty;
		}
	}
}
