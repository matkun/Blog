using Generic.Core.Presentation;
using Generic.Core.Presentation.StartPage;

namespace Generic.Web
{
    public partial class Default : PresentedPageBase<StartPagePresenter>, IStartPageView
    {
        public string ImportantHeading
        {
            get { return litImportantHeading.Text; }
            set { litImportantHeading.Text = value; }
        }

        public string ImportantText
        {
            get { return litImportantText.Text; }
            set { litImportantText.Text = value; }
        }
    }
}
