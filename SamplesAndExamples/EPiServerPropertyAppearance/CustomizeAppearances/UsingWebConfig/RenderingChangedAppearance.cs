using System.Web.UI.WebControls;
using EPiServer.Web.PropertyControls;

namespace EPiServerBuiltInProperties.CustomizeAppearances.UsingWebConfig
{
    public class RenderingChangedAppearance : PropertyStringControl
    {
        public override void CreateDefaultControls()
        {
            this.Controls.Add(new Literal
                {
                    Text = string.Concat(this.ToWebString(),
                    " appearance changed using Web.Config.")
                });

        }
    }
}
