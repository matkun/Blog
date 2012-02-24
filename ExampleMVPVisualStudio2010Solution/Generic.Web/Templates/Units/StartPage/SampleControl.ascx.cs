using Generic.Core.Presentation;
using Generic.Core.Presentation.SampleControl;

namespace Generic.Web.Templates.Units.StartPage
{
    public partial class SampleControl : PresentedControlBase<SampleControlPresenter>, ISampleControlView
    {
        public string ControlText
        {
            get { return litControlText.Text; } 
            set { litControlText.Text = value; }
        }
    }
}
