using System;
using System.IO;
using System.Web;
using System.Web.UI;
using EPiServer.Core;
using PageTypeBuilder.UI;

namespace EPiServer.CodeSample
{
    public partial class StaticError : TemplatePage<StaticErrorPage>
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            DataFactory.Instance.PublishedPage += OnPublishedPage_PublishStaticVersion;
        }

        private void OnPublishedPage_PublishStaticVersion(object sender, PageEventArgs e)
        {
            const string outputFile = @"E:\Projekt\EPiServer\MyEPiServerSite\error.html"; //@"C:\EPiServer\MyEPiServerSite\error.html";
            var page = (StaticErrorPage)e.Page;
            
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
            using (var streamWriter = File.CreateText(outputFile))
            {
                streamWriter.WriteLine(GenerateStaticHtml(page.PageLink));
                streamWriter.Flush();
            }
        }

        private string GenerateStaticHtml(PageReference pageRef)
        {
            var pageData = GetPage(pageRef);
            var uri = new Uri(Configuration.Settings.Instance.SiteUrl, pageData.LinkURL);
            
            var stringWriter = new StringWriter();
            var pageVirtualPath = uri.LocalPath.TrimStart('/');

            var workerRequest = new FakeHttpWorkerRequest
                                    {
                                        ApplicationPhysicalPath = HttpContext.Current.Request.PhysicalApplicationPath,
                                        ApplicationVirtualPath = Configuration.Settings.Instance.SiteUrl.LocalPath,
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
