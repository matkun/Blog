using System.Collections.Generic;

namespace PageTypeTreeFilter.Framework.Authorization
{
    public interface IRoleStrategy
    {
        bool IsAdministrator();
        IEnumerable<string> AdministratorRoles();
    }
}
