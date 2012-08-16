using EPiServer.ClientScript;
using EPiServer.ImageMap.Core;
using EPiServer.ImageMap.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EPiServer.ImageMap.Web.ImageMapProperty
{
    public partial class ImageMapEditModeControl : UserControlBase, IPropertyImageMapEditModeView
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            RegisterClientScriptFile("/ImageMap/Web/js/jquery-1.7.2.min.js");
            RegisterClientScriptFile("/ImageMap/Web/js/jquery.json.min.js");
            RegisterClientScriptFile("/ImageMap/Web/js/json2.js"); // Fixes problem with JSON property not existing in IE global object.
            RegisterClientScriptFile("/ImageMap/Web/js/image-map.js");
        }

        protected string UniqueIdentifier
        {
            get { return ClientScriptUtility.ToScriptSafeIdentifier(ClientID); }
        }

        private string HiddenFieldId
        {
            get { return string.Concat("hidden-updated-map-", UniqueIdentifier); }
        }

        protected string EmptyImageMap
        {
            get { return JsonConvert.SerializeObject(Core.Domain.ImageMap.Empty, new IsoDateTimeConverter()); }
        }

        private string _initialImageMap;
        public string InitialImageMap
        {
            get { return _initialImageMap.EscapedJson(); }
            set { _initialImageMap = Request.Form[HiddenFieldId] ?? value; }
        }

        public string UpdatedImageMap
        {
            get { return Request.Form[HiddenFieldId]; }
        }

        protected string ImageMapEditorUrl { get { return "/ImageMap/Web/ImageMapProperty/HotSpotEditor.aspx"; } }
    }
}
