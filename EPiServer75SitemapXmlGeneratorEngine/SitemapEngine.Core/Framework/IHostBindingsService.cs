using System;
using System.Collections.Generic;

namespace SitemapEngine.Core.Framework
{
	public interface IHostBindingsService
	{
	    Uri DefaultHost();
        string[] AllIetfLanguageTags();
        string IetfLanguageTagFor(Uri uri);
        Dictionary<string, Uri> AllBindings();
	}
}
