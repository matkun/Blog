using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PageTypeTreeFilter.AdapterPattern;
using PageTypeTreeFilter.Extensions;
using PageTypeTreeFilter.Framework.EmbeddedResources;
using PageTypeTreeFilter.Framework.Language;
using PageTypeTreeFilter.Framework.Logging;
using log4net;

namespace PageTypeTreeFilter.Presentation.Settings
{
    public class PageTypePickerPresenter : PresenterBase
    {
        private readonly IPageTypePickerView _view;
        private readonly ITranslator _translator;
        private readonly IResourceHandler _resourceHandler;
        private readonly IPageTypeWrapper _pageType;
        private readonly HttpContextBase _context;
        private readonly ILog _log;

        public PageTypePickerPresenter(
            IPageTypePickerView view,
            ITranslator translator,
            IResourceHandler resourceHandler,
            IPageTypeWrapper pageType,
            HttpContextBase context)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (translator == null) throw new ArgumentNullException("translator");
            if (resourceHandler == null) throw new ArgumentNullException("resourceHandler");
            if (pageType == null) throw new ArgumentNullException("pageType");
            if (context == null) throw new ArgumentNullException("context");
            _view = view;
            _translator = translator;
            _resourceHandler = resourceHandler;
            _pageType = pageType;
            _context = context;
            _log = Log.For(this);
        }

        public override void Init()
        {
            base.Init();
            _resourceHandler.AddPageTypePickerStyleSheet(_view.CurrentPage);
        }

        public override void Load()
        {
            base.Load();

            _view.PageTypePickerDescription = _translator.Translate("/PageTypeTreeFilter/Units/PageTypePicker/Description");

            _view.AvailablePageTypesLabel = _translator.Translate("/PageTypeTreeFilter/Units/PageTypePicker/AvailableLabel");
            _view.AvailablePageTypesDescription = _translator.Translate("/PageTypeTreeFilter/Units/PageTypePicker/AvailableDescription");
            _view.SelectedPageTypesLabel = _translator.Translate("/PageTypeTreeFilter/Units/PageTypePicker/SelectedLabel");
            _view.SelectedPageTypesDescription = _translator.Translate("/PageTypeTreeFilter/Units/PageTypePicker/SelectedDescription");

            _view.AddPageTypeToolTip = _translator.Translate("/PageTypeTreeFilter/Units/PageTypePicker/AddPageTypeToolTip");
            _view.RemovePageTypeToolTip = _translator.Translate("/PageTypeTreeFilter/Units/PageTypePicker/RemovePageTypeToolTip");
        }

        public override void PreRender()
        {
            base.PreRender();

            _view.SelectedPageTypes = GetSelectedPageTypes();
            _view.AvailablePageTypes = AllPageTypes().Where(t => !IsASelectedPageType(t));
        }

        public bool IsASelectedPageType(ListItem item)
        {
            return _view.SelectedPageTypes.Any(selected => selected.Value == item.Value);
        }

        public IEnumerable<ListItem> GetSelectedPageTypes()
        {
            var ids = _view.SelectedPageTypeIds.Split(';');
            foreach (var id in ids)
            {
                int pageTypeId;
                if (!Int32.TryParse(id, out pageTypeId))
                {
                    _log.WarnFormat(
                        "User '{0}' has encountered an invalid PageTypeId '{1}' while selecting page types; this really should not happen.",
                        _context.User.Identity.Name, id);
                    continue;
                }
                yield return _pageType.Load(pageTypeId).ToListItem();
            }
        }

        private IEnumerable<ListItem> AllPageTypes()
        {
            return _pageType.List().ToListItems();
        }
    }
}
