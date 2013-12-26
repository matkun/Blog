using System.Security.Principal;

namespace EPiServer.CodeSample.BookmarkLinks.HtmlGenerator
{
    public class FakeIdentity : IIdentity
    {
        public string Name { get { return string.Empty; } }
        public string AuthenticationType { get { return string.Empty; } }
        public bool IsAuthenticated { get { return false; } }
    }
}
