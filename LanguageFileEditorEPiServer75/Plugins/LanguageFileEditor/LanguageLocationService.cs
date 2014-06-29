using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public class LanguageLocationService : ILanguageLocationService
	{
		private readonly HttpContextBase _context;

		public LanguageLocationService(HttpContextBase context)
		{
			if (context == null) throw new ArgumentNullException("context");
			_context = context;
		}

		private string _languagePath;
		public string LanguagePath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_languagePath))
				{
					_languagePath = GetLanguagePath();
				}
				return _languagePath;
			}
		}

		public string PathTo(string filename)
		{
			return Path.Combine(LanguagePath, filename);
		}

		private string GetLanguagePath()
		{
			var providers = EPiServer.Framework.Configuration.EPiServerFrameworkSection.Instance.Localization.Providers;
			var path = providers.Cast<ProviderSettings>()
				.Select(settings => settings.Parameters)
				.Select(parameters => parameters.Get("virtualPath"))
				.FirstOrDefault(p => !string.IsNullOrEmpty(p));

			if (!string.IsNullOrWhiteSpace(path))
			{
			    return _context.Server.MapPath(path);
			}

			path = !string.IsNullOrWhiteSpace(path) ? path : providers.Cast<ProviderSettings>()
				.Select(settings => settings.Parameters)
				.Select(parameters => parameters.Get("physicalPath"))
				.FirstOrDefault(p => !string.IsNullOrEmpty(p)) ?? string.Empty;

		    return path;
		}
	}
}
