using System.Security.Principal;

namespace PageTypeTreeFilter.Test.Unit.Framework.Authorization
{
    public class FakeIdentity : IIdentity
    {
        public string Name
        {
            get { return "FakeName"; }
        }

        public string AuthenticationType
        {
            get { return "FakeAuthentication"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}
