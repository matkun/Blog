using System;
using EPiServer.Core;
using Generic.Core.Framework;

namespace Generic.Core.Presentation
{
    public abstract class PresentedPageBase<TPresenter> : PresentedPageBase<TPresenter, PageData> where TPresenter : PresenterBase {}
    public abstract class PresentedPageBase<TPresenter, TPage> : TemplatePageBase<TPage>, IView where TPresenter : PresenterBase where TPage : PageData
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
            return IoC.GetPresenter<TPresenter>(this);
        }
    }
}
