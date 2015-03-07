using System;

namespace SitemapEngine.Core.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public class ExcludeFromSitemapAttribute : Attribute { }
}
