using System;
using System.Web;
using System.Web.UI;
using EPiServer.UI;
using PageTypeTreeFilter.Framework.IoC;

namespace PageTypeTreeFilter.Presentation
{
    public abstract class PresentedSystemPageBase<TPresenter> : SystemPageBase, IView
        where TPresenter : PresenterBase
    {
        private TPresenter _presenter;

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            _presenter = CreatePresenter();
            _presenter.PreInit();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                _presenter.FirstTimeInit();
            }
            _presenter.Init();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                _presenter.FirstTimeLoad();
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

        public virtual Page CurrentPage
        {
            get { return Page; }
        }

        public virtual Uri Uri
        {
            get { return HttpContext.Current.Request.Url; }
        }
    }
}
