using System;

namespace SitemapEngine.Core.Extensions
{
	public static class DateTimeExtensions
	{
		public static string FormatWithUtc(this DateTime dateTime)
		{
			var timeDifference = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
			var sign = timeDifference.Hours >= 0 ? "+" : "-";
			var hours = timeDifference.Hours.ToString("00");
			var offset = string.Concat(sign, hours, ":00");

			return string.Concat(dateTime.ToString("yyyy-MM-ddTHH:mm:ss"), offset);
		}
	}
}
