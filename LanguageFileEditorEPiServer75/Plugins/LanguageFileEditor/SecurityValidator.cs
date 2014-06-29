using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using log4net;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public class SecurityValidator : ISecurityValidator
	{
		private static readonly ILog AuditLogger = LogManager.GetLogger(typeof(SecurityValidator));

		private readonly ILanguageLocationService _languageLocationService;
		private readonly HttpContextBase _context;

		private readonly IEnumerable<string> _validDirectories;
		private readonly IEnumerable<string> _validExtensions = new[]
				{
					".xml"
				};
		private readonly IEnumerable<string> _validRoles = new[]
				{
					"WebAdmins",
					"Administrators"
				};

		public SecurityValidator(
			ILanguageLocationService languageLocationService,
			HttpContextBase context)
		{
			if (languageLocationService == null) throw new ArgumentNullException("languageLocationService");
			if (context == null) throw new ArgumentNullException("context");
			_languageLocationService = languageLocationService;
			_context = context;

			_validDirectories = new[]
				{
					Normalize(_languageLocationService.LanguagePath.TrimEnd(Path.DirectorySeparatorChar))
				};
		}

		public void EnsureValidUser()
		{
			if (_validRoles.Any(role => _context.User.IsInRole(role)))
			{
				return;
			}
			var message = string.Format("Unauthorized access attempt to URL '{0}', see corresponding log handler for user IP etc.", _context.Request.Url);
			AuditLogger.Warn(message);
			throw new UnauthorizedAccessException("Unauthorized access attempt to path.");
		}

		public void EnsureValid(string path)
		{
			var normalizedPath = Normalize(path);
			if (IsFile(normalizedPath))
			{
				var file = normalizedPath
							.Split(Path.DirectorySeparatorChar)
							.LastOrDefault();
				EnsureValidFile(file);
				normalizedPath = normalizedPath.TrimEnd(file);
			}
			EnsureValidDirectory(normalizedPath);
		}

		private void EnsureValidDirectory(string normalizedPath)
		{
			normalizedPath = normalizedPath.TrimEnd(Path.DirectorySeparatorChar);
			if (_validDirectories.Any(p => p.Equals(normalizedPath)))
			{
				return;
			}
			var message = string.Format("Unauthorized access attempt to path '{0}'.", normalizedPath);
			AuditLogger.Warn(message);
			throw new UnauthorizedAccessException(message);
		}

		private void EnsureValidFile(string file)
		{
			if (_validExtensions.Any(file.EndsWith))
			{
				return;
			}
			var message = string.Format("Unauthorized access attempt to forbidden file '{0}'.", file);
			AuditLogger.Warn(message);
			throw new UnauthorizedAccessException(message);
		}

		private static bool IsFile(string path)
		{
			var attributes = File.GetAttributes(path);
			return (attributes & FileAttributes.Directory) != FileAttributes.Directory;
		}

		private static string Normalize(string path)
		{
			return Path.GetFullPath(new Uri(path).LocalPath)
					   .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
					   .ToLowerInvariant();
		}
	}
}
