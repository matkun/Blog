using System;
using System.Web;
using EPiServer;
using EPiServer.Core;
using Generic.Core.Extensions;

namespace Generic.Core.Presentation
{
    public abstract class TemplatePageBase<TPage> : TemplatePage where TPage : PageData
    {
        public new TPage CurrentPage
        {
            get { return this.CurrentPage<TPage>(); }
        }

        public StartPage.StartPage StartPage
        {
            get
            {
                return (StartPage.StartPage)GetPage(PageReference.StartPage);
            }
        }

        public virtual Uri Uri
        {
            get { return HttpContext.Current.Request.Url; }
        }
    }
}
