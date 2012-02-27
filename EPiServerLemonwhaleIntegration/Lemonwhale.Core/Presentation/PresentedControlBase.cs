using System;
using System.Web;
using System.Web.UI;
using Lemonwhale.Core.Framework;
using Lemonwhale.Core.Framework.IoC;

namespace Lemonwhale.Core.Presentation
{
    public class PresentedControlBase<TPresenter> : UserControl, IView
        where TPresenter : PresenterBase
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
