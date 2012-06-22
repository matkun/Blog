using System;
using EPiServer;
using EPiServer.UI;

namespace ImageSlideShow.Core.Presentation.MasterPages
{
    public partial class Plugin : MasterPageBase
    {
        protected override void OnInit(EventArgs e)
        {
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUtilBySettings("javascript/episerverscriptmanager.js"));
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUIBySettings("javascript/system.js"));
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUIBySettings("javascript/system.aspx"));
        }
    }
}
