using System;
using System.Text;
using System.Web.Mvc;
using System.Xml;

namespace SitemapEngine.Core.Infrastructure
{
	public class XmlActionResult : ActionResult
	{
		private readonly XmlDocument _document;

		public Formatting Formatting { get; set; }
		public string MimeType { get; set; }

		public XmlActionResult(XmlDocument document)
		{
			if (document == null)
			{
			    throw new ArgumentNullException("document");
			}

			_document = document;
			MimeType = "text/xml";
			Formatting = Formatting.None;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.Clear();
			context.HttpContext.Response.ContentType = MimeType;
			context.HttpContext.Response.Expires = 0;

			using (var writer = new XmlTextWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8) {Formatting = Formatting})
			{
				_document.WriteTo(writer);
			}
		}
	}
}
