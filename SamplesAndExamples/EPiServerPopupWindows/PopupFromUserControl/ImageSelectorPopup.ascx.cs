using System;

namespace EPiServer.CodeSample.Popups
{
    public partial class ImageSelectorPopup : EPiServer.UserControlBase
    {
        protected override void OnInit(EventArgs e)
        {
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUtilBySettings("javascript/episerverscriptmanager.js"));
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUIBySettings("javascript/system.js"));
            RegisterClientScriptFile(UriSupport.ResolveUrlFromUIBySettings("javascript/system.aspx"));
            RegisterClientScriptFile("https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js");
            RegisterClientScriptFile("ImageSelector.js");
        }

        protected string UniqueIdentifier
        {
            get { return EPiServer.ClientScript.ClientScriptUtility.ToScriptSafeIdentifier(ClientID); }
        }
    }
}
