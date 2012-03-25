using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace PageTypeTreeFilter.Test.Unit.Framework.Authorization
{
    public class FakeUser : IPrincipal
    {
        public IEnumerable<string> UserRoles { get; set; }

        public FakeUser()
        {
            UserRoles = new List<string>();
        }

        public bool IsInRole(string role)
        {
            return UserRoles.Contains(role);
        }

        public IIdentity Identity
        {
            get { return new FakeIdentity(); }
        }
    }
}
