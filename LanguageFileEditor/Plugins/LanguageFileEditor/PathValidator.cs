using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EPiServer.Core;
using log4net;

namespace EPiServer.Plugins.LanguageFileEditor
{
    public static class PathValidator
    {
        // You could let the exceptions thrown in this class be unhandled, and have
        // ELMAH give you usernames, IP-addresses, and so forth.
        private static readonly ILog AuditLogger = LogManager.GetLogger(typeof(PathValidator));

        // No, these should _not be in a config file.
        private static readonly IEnumerable<string> ValidDirectories = new[]
            {
                Normalize(LanguageManager.Instance.Directory.TrimEnd(System.IO.Path.DirectorySeparatorChar))
            };
        private static readonly IEnumerable<string> ValidExtensions = new[]
            {
                ".xml"
            };
        
        public static void EnsureValid(string path)
        {
            var normalizedPath = Normalize(path);
            if(IsFile(normalizedPath))
            {
                var file = normalizedPath
                            .Split(System.IO.Path.DirectorySeparatorChar)
                            .LastOrDefault();
                EnsureValidFile(file);
                normalizedPath = normalizedPath.TrimEnd(file);
            }
            EnsureValidDirectory(normalizedPath);
        }

        private static void EnsureValidDirectory(string normalizedPath)
        {
            normalizedPath = normalizedPath.TrimEnd(System.IO.Path.DirectorySeparatorChar);
            if (ValidDirectories.Any(p => p.Equals(normalizedPath)))
            {
                return;
            }
            var message = string.Format("Unauthorized access attempt to path '{0}'.", normalizedPath);
            AuditLogger.Warn(message);
            throw new UnauthorizedAccessException(message);
        }

        private static void EnsureValidFile(string file)
        {
            if(ValidExtensions.Any(file.EndsWith))
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
            return System.IO.Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)
                       .ToLowerInvariant();
        }
    }
}
