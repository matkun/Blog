using System;
using EPiServer.UI;

namespace EPiServer.ImageMap.Web.ImageMapProperty
{
    public partial class HotSpotEditor : SystemPageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // TODO - These might go well in a MasterPage if you have multiple plug-ins that need them.
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUtilBySettings("javascript/episerverscriptmanager.js"));
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUIBySettings("javascript/system.js"));
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUIBySettings("javascript/system.aspx"));
            RegisterClientScriptFile("/ImageMap/Web/js/jquery-1.7.2.min.js");
            RegisterClientScriptFile("/ImageMap/Web/js/jquery-ui-1.8.22.custom.min.js");
            RegisterClientScriptFile("/ImageMap/Web/js/jquery.json.min.js");
        }
    }
}
