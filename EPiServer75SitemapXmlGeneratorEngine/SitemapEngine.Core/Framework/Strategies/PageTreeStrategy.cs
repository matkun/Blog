using System;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using SitemapEngine.Core.Domain;
using SitemapEngine.Core.Extensions;
using SitemapEngine.Core.Infrastructure;
using SitemapEngine.Core.Wrappers;

namespace SitemapEngine.Core.Framework.Strategies
{
    /// <summary>
    /// Sample strategy for indexing EPiServer pages. Recursively adding entries for all published
    /// pages with proper access level, that are of page types NOT decorated with the [ExcludeFromSitemap]
    /// attribute. Also offers a dynamic property 'IncludeInSitemap' checkbox for excluding/including
    /// specific page instances.
    /// </summary>
    public class PageTreeStrategy : ISitemapStrategy
    {
        private readonly IContentLoader _contentLoader;
        private readonly IFilterAccessWrapper _filterAccessWrapper;
        private readonly IHostBindingsService _hostBindingsService;

        public PageTreeStrategy(
            IContentLoader contentLoader,
            IFilterAccessWrapper filterAccessWrapper,
            IHostBindingsService hostBindingsService)
        {
            if (contentLoader == null) throw new ArgumentNullException("contentLoader");
            if (filterAccessWrapper == null) throw new ArgumentNullException("filterAccessWrapper");
            if (hostBindingsService == null) throw new ArgumentNullException("hostBindingsService");
            _contentLoader = contentLoader;
            _filterAccessWrapper = filterAccessWrapper;
            _hostBindingsService = hostBindingsService;
        }

        public void ForEach(Action<SitemapEntry> add)
        {
            var root = _contentLoader.Get<PageData>(ContentReference.StartPage);
            foreach (var language in _hostBindingsService.AllIetfLanguageTags())
            {
                AddEntriesRecursiveFrom(root, new LanguageSelector(language), add);
            }
        }

        private void AddEntriesRecursiveFrom(PageData page, LanguageSelector language, Action<SitemapEntry> add)
        {
            if (ShowInSiteMap(page))
            {
                add(page.ToSitemapEntry(language, Constants.Bundles.PageTree));
            }

            var children = _contentLoader.GetChildren<PageData>(page.PageLink, language);
            foreach (var child in children)
            {
                AddEntriesRecursiveFrom(child, language, add);
            }
        }

        private bool ShowInSiteMap(PageData page)
        {
            //// Might want to only expose project page types.
            //if(!(page is MyProjectPageBase))
            //{
            //    return false;
            //}
            if (!page.IsIndexablePageType())
            {
                return false;
            }
            if (!page.CheckPublishedStatus(PagePublishedStatus.Published))
            {
                return false;
            }
            if (!_filterAccessWrapper.QueryDistinctAccessEdit(page, AccessLevel.NoAccess))
            {
                return false;
            }
            //// Dynamic property: Checkbox for including/excluding page instance in sitemap.xml. For an overridable dynamic property checkbox
            //// wrapper, see https://blog.mathiaskunto.com/2015/02/09/custom-property-checkbox-wrapper-as-dynamic-property-in-episerver-7-5/
            //if (!(page as MyProjectPageBase).IncludeInSitemap)
            //{
            //    return false;
            //}
            return true;
        }
    }
}
