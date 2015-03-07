using System;

namespace SitemapEngine.Core.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public class ValueAttribute : Attribute
	{
		public string Value { get; set; }

		public ValueAttribute(string value)
		{
			Value = value;
		}
	}
}
