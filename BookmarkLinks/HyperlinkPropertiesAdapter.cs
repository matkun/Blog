using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using EPiServer.ClientScript;
using EPiServer.ClientScript.Events;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;
using EPiServer.Web.WebControls;

//<package id="HtmlAgilityPack" version="1.4.6" targetFramework="net40" />
//<adapter controlType="EPiServer.UI.Editor.Tools.Dialogs.HyperlinkProperties" adapterType="EPiServer.CodeSample.BookmarkLinks.HyperlinkPropertiesAdapter" />

[assembly: WebResource("EPiServer.CodeSample.BookmarkLinks.BookmarkLinks.js", "text/javascript")]
[assembly: WebResource("EPiServer.CodeSample.BookmarkLinks.BookmarkLinks.css", "text/css")]
namespace EPiServer.CodeSample.BookmarkLinks
{
    public class HyperlinkPropertiesAdapter : PageAdapter
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AddJQuery();
            AddHashTagLinksJavaScript();
            AddHashTagLinksStylesheet();
        }

        protected override void OnLoad(EventArgs e)
        {
            // We don't want to modify EPiServer's code if it's not a PostBack.
            if (!Page.IsPostBack)
            {
                base.OnLoad(e);
                return;
            }
            
            var linkinternalurl = Control.FindControlRecursive<InputPageReference>("linkinternalurl");
            var linklanguages = Control.FindControlRecursive<DropDownList>("linklanguages");
            var activeTab = Control.FindControlRecursive<HtmlInputHidden>("activeTab");
            var linktypeinternal = Control.FindControlRecursive<HtmlInputRadioButton>("linktypeinternal");

            var request = HttpContext.Current.Request;

            // Modified parts of EPiServer's HyperlinkProperties.aspx.cs load method below
            var function = "function CloseAfterPostback(e) {";
            if (String.CompareOrdinal(activeTab.Value, "0") == 0 && linktypeinternal.Checked)
            {
                var urlBuilder = new UrlBuilder(DataFactory.Instance.GetPage(linkinternalurl.PageLink).StaticLinkURL);
                var linkLang = request.Form[linklanguages.UniqueID];
                if (!string.IsNullOrEmpty(linkLang))
                {
                    urlBuilder.QueryCollection["epslanguage"] = linkLang;
                }
                var ddlSelectedBookmark = request["ddlBookmarkSelector"];
                var bookmarkOrDefault = !BookmarkLocator.DefaultBookmark.Equals(ddlSelectedBookmark) ? string.Concat("#", ddlSelectedBookmark) : string.Empty;
                function = function + "EPi.GetDialog().returnValue.href = '" + urlBuilder + bookmarkOrDefault + "';";
            }
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "closeafterpostback", function + "EPi.GetDialog().Close(EPi.GetDialog().returnValue);}", true);
            ((PageBase)Page).ScriptManager.AddEventListener(Page, new CustomEvent(EventType.Load, "CloseAfterPostback"));
        }

        private void AddJQuery()
        {
            // TODO - You might want to get jQuery support from somewhere else.
            var js = new HtmlGenericControl("script");
            js.Attributes.Add("src", "http://code.jquery.com/jquery-1.7.2.min.js");
            js.Attributes.Add("type", "text/javascript");
            Page.Header.Controls.Add(js);
        }

        private void AddHashTagLinksJavaScript()
        {
            var jsPath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(HyperlinkPropertiesAdapter), "EPiServer.CodeSample.BookmarkLinks.BookmarkLinks.js");

            var js = new HtmlGenericControl("script");
            js.Attributes.Add("src", jsPath);
            js.Attributes.Add("type", "text/javascript");
            Page.Header.Controls.Add(js);
        }

        private void AddHashTagLinksStylesheet()
        {
            var cssPath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(HyperlinkPropertiesAdapter), "EPiServer.CodeSample.BookmarkLinks.BookmarkLinks.css");

            var cssLink = new HtmlLink { Href = cssPath };
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("media", "screen");
            Page.Header.Controls.Add(cssLink);
        }
    }
}
