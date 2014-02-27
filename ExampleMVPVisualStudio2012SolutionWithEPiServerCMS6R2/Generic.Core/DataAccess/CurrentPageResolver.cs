using System;
using EPiServer.Core;
using Generic.Core.Framework;

namespace Generic.Core.DataAccess
{
    public class CurrentPageResolver : ICurrentPageResolver
    {
        public T CurrentPage<T>(IPageSource page) where T : PageData
        {
            var cPage = page.CurrentPage;
            if (cPage == null) return null;
            var typedPage = cPage as T;
            if (typedPage != null) return typedPage;
            if (cPage.PageLink == PageReference.StartPage) return IoC.Get<T>();
            var msg = string.Format("Invalid PageType on page '{0}', a page of type '{1}' was expected, but got '{2}'.",
                cPage.PageLink, typeof(T).FullName, cPage.GetType().FullName);
            throw new InvalidOperationException(msg);
        }
    }
}
