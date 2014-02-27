using EPiServer.Core;
using Generic.Core.Extensions;

namespace Generic.Core.Presentation
{
    public abstract class UserControlBase : UserControlBase<PageData> {}

    public abstract class UserControlBase<TPageData> : EPiServer.UserControlBase, IPageSource where TPageData : PageData
    {
        private TPageData _currentPage;
        
        public virtual new TPageData CurrentPage
        {
            get { return _currentPage ?? CurrentPageFromContainer; }
            set
            {
                _currentPage = value;
                base.CurrentPage = value;
            }
        }

        public StartPage.StartPage StartPage
        {
            get
            {
                return (StartPage.StartPage)GetPage(PageReference.StartPage);
            }
        }

        private TPageData CurrentPageFromContainer
        {
            get { return this.CurrentPage<TPageData>(); }
        }
    }
}
