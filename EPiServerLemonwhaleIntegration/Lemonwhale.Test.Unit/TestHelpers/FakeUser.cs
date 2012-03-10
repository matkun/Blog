using System.Security.Principal;

namespace Lemonwhale.Test.Unit.TestHelpers
{
    public static class FakeUser
    {
        public static IPrincipal ThatIsAuthenticated()
        {
            return new AuthenticatedFakeUser();
        }

        private class AuthenticatedFakeUser : IPrincipal
        {
            public bool IsInRole(string role)
            {
                return role == "true";
            }

            public IIdentity Identity
            {
                get { return new AuthenticatedFakeIdentity(); }
            }
        }

        private class AuthenticatedFakeIdentity : IIdentity
        {
            public string Name
            {
                get { return "Fake user"; }
            }

            public string AuthenticationType
            {
                get { return "Fake authentication type"; }
            }

            public bool IsAuthenticated
            {
                get { return true; }
            }
        }
    }
}
