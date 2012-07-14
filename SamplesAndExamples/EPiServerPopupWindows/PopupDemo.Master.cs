using System;
using EPiServer.UI;

namespace EPiServer.CodeSample.Popups
{
    public partial class PopupDemo : MasterPageBase
    {
        protected override void OnInit(EventArgs e)
        {
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUtilBySettings("javascript/episerverscriptmanager.js"));
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUIBySettings("javascript/system.js"));
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUIBySettings("javascript/system.aspx"));
            RegisterClientScriptFile("https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js");
        }
    }
}
