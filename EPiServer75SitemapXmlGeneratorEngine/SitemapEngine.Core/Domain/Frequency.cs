using SitemapEngine.Core.CustomAttributes;

namespace SitemapEngine.Core.Domain
{
	public enum Frequency
	{
		[Value("unspecified")]
		None = 0,

		[Value("always")]
		Always = 1,

		[Value("hourly")]
		Hourly = 2,

		[Value("daily")]
		Daily = 3,

		[Value("weekly")]
		Weekly = 4,

		[Value("monthly")]
		Monthly = 5,

		[Value("yearly")]
		Yearly = 6,

		[Value("never")]
		Never = 7
	}
}
