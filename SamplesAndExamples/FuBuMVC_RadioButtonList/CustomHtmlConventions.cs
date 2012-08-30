using FubuMVC.Core.UI;
using MyProject.Core.Infrastructure.Fubu.HtmlConventions;

namespace MyProject.Core.Infrastructure.Fubu
{
    public class CustomHtmlConventions : HtmlConventionRegistry
    {
        public CustomHtmlConventions()
        {
            Displays.Builder<EnumRadioButtonListDisplay>();
            Editors.Builder<EnumRadioButtonListEditor>();
        }
    }
}
