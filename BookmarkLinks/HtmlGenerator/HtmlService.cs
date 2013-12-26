using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using EPiServer.Core;

namespace EPiServer.CodeSample.BookmarkLinks.HtmlGenerator
{
    public class HtmlService
    {
        public string HtmlFor(PageReference pageRef)
        {
            var pageData = DataFactory.Instance.GetPage(pageRef);
            var siteUrl = Configuration.Settings.Instance.SiteUrl;
            var uri = new Uri(siteUrl, pageData.LinkURL);

            var stringWriter = new StringWriter();
            var pageVirtualPath = uri.LocalPath.TrimStart('/');

            var workerRequest = new FakeHttpWorkerRequest
                {
                    ApplicationPhysicalPath = HttpContext.Current.Request.PhysicalApplicationPath,
                    ApplicationVirtualPath = siteUrl.LocalPath,
                    PageVirtualPath = pageVirtualPath,
                    QueryString = uri.Query,
                    OutputTextWriter = stringWriter
                };

            var realHttpContext = HttpContext.Current;
            HttpContext.Current = new HttpContext(workerRequest)
                {
                    User = new FakePrincipal()
                };

            var pageFileName = uri.Segments[uri.Segments.Length - 1];
            var pagePhysicalPath = HttpContext.Current.Server.MapPath(pageFileName);
            HttpContext.Current.Handler = PageParser.GetCompiledPageInstance(pageVirtualPath, pagePhysicalPath, HttpContext.Current);
            HttpContext.Current.Handler.ProcessRequest(HttpContext.Current);
            HttpContext.Current.Response.Flush();
            HttpContext.Current = realHttpContext;

            var externalUrl = new UrlBuilder(uri);
            Global.UrlRewriteProvider.ConvertToExternal(externalUrl, pageData.PageLink, System.Text.Encoding.UTF8);
            var htmlRewriter = Global.UrlRewriteProvider.GetHtmlRewriter();
            var internalUrl = new UrlBuilder(uri);
            var html = htmlRewriter.RewriteString(internalUrl, externalUrl, System.Text.Encoding.UTF8, stringWriter.GetStringBuilder().ToString());

            return html;
        }
    }
}
