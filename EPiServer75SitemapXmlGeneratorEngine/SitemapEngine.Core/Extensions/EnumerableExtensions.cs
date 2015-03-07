using System.Collections.Generic;
using System.Linq;

namespace SitemapEngine.Core.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence)
		{
			return sequence == null || !sequence.Any();
		}
	}
}
