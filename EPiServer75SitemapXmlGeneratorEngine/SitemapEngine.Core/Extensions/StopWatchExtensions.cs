using System.Diagnostics;

namespace SitemapEngine.Core.Extensions
{
	public static class StopWatchExtensions
	{
		public static string ToElapsedTimeString(this Stopwatch timer)
		{
			var time = timer.Elapsed;
			return time.Ticks == 0 ? string.Empty : string.Format("{0} hour(s), {1} minute(s) and {2} second(s).", time.Hours, time.Minutes, time.Seconds);
		}
	}
}
