using System.Web;

namespace EPiServer.CodeSample.BookmarkLinks
{
    public class AvailableBookmarks : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var response = context.Response;
            response.Clear();
            response.ContentType = "text/html";
            var locator = new BookmarkLocator(context);
            response.Write(locator.BookmarkSelector());
            response.End();
        }

        public bool IsReusable { get { return false; } }
    }
}
