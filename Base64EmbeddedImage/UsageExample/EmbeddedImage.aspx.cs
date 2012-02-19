using System;
using PageTypeBuilder.UI;

namespace EPiServer.CodeSample
{
    public partial class EmbeddedImage : TemplatePage<EmbeddedImagePage>
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            embeddedBase64Image.DataBind();
        }
    }
}