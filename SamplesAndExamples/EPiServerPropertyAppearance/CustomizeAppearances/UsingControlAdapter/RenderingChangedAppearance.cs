using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;

namespace EPiServerBuiltInProperties.CustomizeAppearances.UsingControlAdapter
{
    public class RenderingChangedAppearance : ControlAdapter
    {
        protected override void Render(HtmlTextWriter writer)
        {
            Control.Controls
                .Add(new Literal
                    {
                        Text = " appearance changed using a ControlAdapter."
                    });
            base.Render(writer);
        }
    }
}
