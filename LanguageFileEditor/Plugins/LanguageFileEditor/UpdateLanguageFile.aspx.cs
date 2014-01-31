using System;
using System.IO;
using System.Text;
using System.Web.UI;
using Newtonsoft.Json.Linq;

namespace EPiServer.Plugins.LanguageFileEditor
{
    public partial class UpdateLanguageFile : Page
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Response.Expires = -1;
            Response.ContentType = "application/json";
            if(!Request.ContentType.Contains("json"))
            {
                Response.Write("Request does not contain JSON data.");
                Response.End();
                return;
            }

            var streamReader = new StreamReader(Request.InputStream, Encoding.UTF8);
            var jsonString = streamReader.ReadToEnd();
            var jObject = JObject.Parse(jsonString);

            var languageFileUpdater = new LanguageFileUpdater { NewContent = (JObject)jObject["xmlContent"] };
            languageFileUpdater.ExecuteApplyFor((string)jObject["targetFilename"], (string)jObject["patternFilename"]);
            Response.Write("{\"Status\":\"200 OK\"}");
            Response.End();
        }
    }
}