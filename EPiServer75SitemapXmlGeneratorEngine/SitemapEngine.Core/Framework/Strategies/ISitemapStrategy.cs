using System;
using SitemapEngine.Core.Domain;

namespace SitemapEngine.Core.Framework.Strategies
{
	public interface ISitemapStrategy
	{
		void ForEach(Action<SitemapEntry> add);
	}
}
