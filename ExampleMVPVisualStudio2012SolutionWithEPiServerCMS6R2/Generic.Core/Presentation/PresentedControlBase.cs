using System;
using System.Web;
using System.Web.UI;
using EPiServer.Core;
using Generic.Core.Framework;

namespace Generic.Core.Presentation
{
    public abstract class PresentedControlBase<TPresenter> : PresentedControlBase<TPresenter, PageData> where TPresenter : PresenterBase {}
    public abstract class PresentedControlBase<TPresenter, TPageData> : UserControlBase<TPageData>, IView<TPageData>
        where TPresenter : PresenterBase
        where TPageData : PageData
    {
        private TPresenter _presenter;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _presenter = CreatePresenter();

            if (!IsPostBack)
            {
                _presenter.FirstTimeInit();
            }

            _presenter.Load();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            _presenter.PreRender();
        }

        protected TPresenter CreatePresenter()
        {
            return IoC.GetPresenter<TPresenter>(this);
        }

        public MasterPage MasterPage
        {
            get { return Page.Master; }
        }

        public Uri Uri
        {
            get { return HttpContext.Current.Request.Url; }
        }
    }
}
