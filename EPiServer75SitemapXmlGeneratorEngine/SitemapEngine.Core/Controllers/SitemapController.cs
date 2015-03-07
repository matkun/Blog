using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using SitemapEngine.Core.Framework;
using SitemapEngine.Core.Infrastructure;

namespace SitemapEngine.Core.Controllers
{
    // TODO: Important stuff getting the sitemap engine to work.
    // Note that you will need MVC 5 on your EPiServer site for this Controller to work as it uses MVC Attribute Routing.
    // Don't forget to map the routes in for instance your Global.asax.cs if you're not already, as below:
    //
    // protected override void RegisterRoutes(RouteCollection routes)
    // {
    //   base.RegisterRoutes(routes);
    //   routes.MapMvcAttributeRoutes();
    //

	public class SitemapController : Controller
	{
		private readonly ISitemapRepository _sitemapRepository;
	    private readonly IHostBindingsService _hostBindingsService;
	    private readonly HttpRequestBase _request;

	    public SitemapController(
            ISitemapRepository sitemapRepository,
            IHostBindingsService hostBindingsService,
            HttpRequestBase request)
		{
			if (sitemapRepository == null) throw new ArgumentNullException("sitemapRepository");
	        if (hostBindingsService == null) throw new ArgumentNullException("hostBindingsService");
	        if (request == null) throw new ArgumentNullException("request");
	        _sitemapRepository = sitemapRepository;
		    _hostBindingsService = hostBindingsService;
	        _request = request;
		}

		[Route("sitemap.xml")]
		public ActionResult Index()
        {
            var language = _hostBindingsService.IetfLanguageTagFor(_request.Url);
            var ms = new MemoryStream(_sitemapRepository.ReadSitemapFor(language));
            var doc = new XmlDocument();
            doc.Load(ms);
            return new XmlActionResult(doc);
		}
	}
}
