using System.Security.Principal;

namespace EPiServer.CodeSample
{
    public class FakePrincipal : IPrincipal
    {
        public bool IsInRole(string role) { return false; }
        public IIdentity Identity { get { return new FakeIdentity(); } }
    }
}