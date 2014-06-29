using System;
using System.IO;
using System.Text;
using System.Web.UI;
using EPiServer.ServiceLocation;
using Newtonsoft.Json.Linq;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	public partial class UpdateLanguageFile : Page
	{
		private readonly ISecurityValidator _securityValidator;
		private readonly ILanguageFileUpdater _languageFileUpdater;
		private readonly ILanguageLocationService _languageLocationService;

		public UpdateLanguageFile()
		{
			_securityValidator = ServiceLocator.Current.GetInstance<ISecurityValidator>();
			_languageFileUpdater = ServiceLocator.Current.GetInstance<ILanguageFileUpdater>();
			_languageLocationService = ServiceLocator.Current.GetInstance<ILanguageLocationService>();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			_securityValidator.EnsureValidUser();

			Response.Expires = -1;
			Response.ContentType = "application/json";
			if (!Request.ContentType.Contains("json"))
			{
				Response.Write("Request does not contain JSON data.");
				Response.End();
				return;
			}

			var streamReader = new StreamReader(Request.InputStream, Encoding.UTF8);
			var jsonString = streamReader.ReadToEnd();
			var jObject = JObject.Parse(jsonString);

			var targetFile = (string)jObject["targetFilename"];
			var patternFile = (string)jObject["patternFilename"];
			_securityValidator.EnsureValid(_languageLocationService.PathTo(targetFile));
			_securityValidator.EnsureValid(_languageLocationService.PathTo(patternFile));

			_languageFileUpdater.NewContent = (JObject)jObject["xmlContent"];
			_languageFileUpdater.ExecuteApplyFor(targetFile, patternFile);

			Response.Write("{\"Status\":\"200 OK\"}");
			Response.End();
		}
	}
}
