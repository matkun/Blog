using System;
using System.Web;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Templates.Alloy.LanguageTool.Wrappers;

namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure
{
    public class TranslationsController<T> : Controller where T : PageData
    {
        private readonly IUrlRewriteProviderWrapper _urlRewriteProvider;
        private readonly IContentLoader _contentLoader;
        private readonly HttpRequestBase _request;

        public TranslationsController()
        {
            _urlRewriteProvider = ServiceLocator.Current.GetInstance<IUrlRewriteProviderWrapper>();
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            _request = ServiceLocator.Current.GetInstance<HttpRequestBase>();
        }

        public T CurrentPage
        {
            get
            {
                var urlBuilder = new UrlBuilder(_request.Url);
                object currentReference;
                if (!_urlRewriteProvider.ConvertToInternal(urlBuilder, out currentReference))
                {
                    throw new Exception(string.Format("Unable to convert url '{0}' to content reference when retrieving CurrentPage of type '{1}'.",
                                            urlBuilder,
                                            typeof(T).Name));
                }
                return _contentLoader.Get<T>((ContentReference)currentReference);
            }
        }
    }
}
