using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace EPiServer.Plugins.LanguageFileEditor
{
    public static class UserValidator
    {
        // You could let the exceptions thrown in this class be unhandled, and have
        // ELMAH give you usernames, IP-addresses, and so forth.
        private static readonly ILog AuditLogger = LogManager.GetLogger(typeof(UserValidator));

        private static readonly IEnumerable<string> ValidRoles = new[]
            {
                "WebAdmins",
                "Administrators"
            };

        public static void EnsureValidRoles()
        {
            var context = HttpContext.Current;
            if (ValidRoles.Any(role => context.User.IsInRole(role)))
            {
                return;
            }
            var message = string.Format("Unauthorized access attempt to URL '{0}', see corresponding log handler for user IP etc.", context.Request.Url);
            AuditLogger.Warn(message);
            throw new UnauthorizedAccessException("Unauthorized access attempt to path.");
        }
    }
}