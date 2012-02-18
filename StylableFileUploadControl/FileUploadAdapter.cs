using System;
using System.Web.UI;
using System.Web.UI.WebControls.Adapters;

[assembly: System.Web.UI.WebResource("StylableFileUploadControl.FileUploadAdapter.js", "application/javascript")]
namespace StylableFileUploadControl
{
    public class FileUploadAdapter : WebControlAdapter
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var jsPath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(FileUploadAdapter), "StylableFileUploadControl.FileUploadAdapter.js");
            Page.ClientScript.RegisterClientScriptInclude("StylableFileUploadControl.FileUploadAdapter.js", jsPath);
        }

        protected override void OnPreRender(EventArgs e)
        {
            Control.Attributes["style"] += "display:none;";
            base.OnPreRender(e);
        }

        protected override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "file-upload");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            base.RenderBeginTag(writer);
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderContents(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "...");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        protected override void RenderEndTag(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
            writer.RenderEndTag(); // file-upload div
        }
    }
}
