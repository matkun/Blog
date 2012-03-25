using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PageTypeTreeFilter.Framework.Logging;
using log4net;

namespace PageTypeTreeFilter.Framework.Authorization
{
    public class RoleStrategy : IRoleStrategy
    {
        private readonly ISettings _settings;
        private readonly HttpContextBase _context;
        private readonly ILog _log;

        public RoleStrategy(ISettings settings,  HttpContextBase context)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            if (context == null) throw new ArgumentNullException("context");
            _settings = settings;
            _context = context;
            _log = Log.For(this);
        }

        public bool IsAdministrator()
        {
            return AdministratorRoles()
                .Any(role => _context.User.IsInRole(role));
        }

        public IEnumerable<string> AdministratorRoles()
        {
            if (string.IsNullOrEmpty(_settings.GlobalSettingsRoles))
            {
                _log.Warn("There are no administrator roles defined for the PageTypeTreeFilter global settings; only these roles may change the global settings.");
                return Enumerable.Empty<string>();
            }
            return _settings.GlobalSettingsRoles.Split(',')
                .Select(role => role.Trim())
                .Where(role => !string.IsNullOrEmpty(role));
        }
    }
}
