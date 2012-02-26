using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.PlugIn;
using EPiServer.Security;
using EPiServer.UI;

namespace EmbeddedEPiServerResources
{
    [GuiPlugIn(
        DisplayName = "Embedded Plugin",
        Description = "Plugin that was embedded and distributed as a binary assembly file.",
        Area = PlugInArea.AdminMenu,
        RequiredAccess = AccessLevel.Administer,
        Url = "~/EmbeddedResource/EmbeddedEPiServerResources.dll/EmbeddedEPiServerResources.EmbeddedPlugin.aspx"
    )]
    public partial class EmbeddedPlugin : SystemPageBase
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            Page.MasterPageFile = EPiServer.Configuration.Settings.Instance.UIUrl + "MasterPages/EPiServerUI.Master";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SystemMessageContainer.Heading = "Embedded Plugin";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UseQueryParameters();
        }

        private void UseQueryParameters()
        {
            var container = new HtmlGenericControl("div");
            foreach (var key in Request.QueryString.AllKeys)
            {
                container
                    .Controls
                    .Add(new Literal
                        {
                            Text = string.Format("<p>Query parameter named <strong>{0}</strong> has value <strong>{1}</strong>.</p>", key, Request.QueryString[key])
                        });
            }
            QueryParameterExample.Controls.Add(container);
        }
    }
}
