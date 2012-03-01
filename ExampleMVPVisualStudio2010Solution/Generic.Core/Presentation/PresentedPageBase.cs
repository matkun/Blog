using System;
using System.Web;
using System.Web.UI;
using Generic.Core.Framework;

namespace Generic.Core.Presentation
{
    public abstract class PresentedPageBase<TPresenter> : Page, IView
        where TPresenter : PresenterBase
    {
        private TPresenter _presenter;

        protected override void OnInit(EventArgs e)
        {
            _presenter = CreatePresenter();
            base.OnInit(e);
            _presenter.Init();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

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
            return IOC.GetPresenter<TPresenter>(this);
        }

        public MasterPage MasterPage
        {
            get { return Page.Master; }
        }

        public virtual Uri Uri
        {
            get { return HttpContext.Current.Request.Url; }
        }
    }
}
