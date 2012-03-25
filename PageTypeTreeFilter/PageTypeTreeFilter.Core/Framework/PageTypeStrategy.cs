using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using PageTypeTreeFilter.AdapterPattern;
using PageTypeTreeFilter.Extensions;
using PageTypeTreeFilter.Framework.DataAccess.GlobalSettings;
using PageTypeTreeFilter.Framework.DataAccess.UserSettings;
using PageTypeTreeFilter.Framework.Language;

namespace PageTypeTreeFilter.Framework
{
    public class PageTypeStrategy : IPageTypeStrategy
    {
        private readonly ITranslator _translator;
        private readonly IGlobalSettingsRepository _globalSettingsRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IPageTypeWrapper _pageType;

        public PageTypeStrategy(
            ITranslator translator,
            IGlobalSettingsRepository globalSettingsRepository,
            IUserSettingsRepository userSettingsRepository,
            IPageTypeWrapper pageType)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            if (globalSettingsRepository == null) throw new ArgumentNullException("globalSettingsRepository");
            if (userSettingsRepository == null) throw new ArgumentNullException("userSettingsRepository");
            if (pageType == null) throw new ArgumentNullException("pageType");
            _translator = translator;
            _globalSettingsRepository = globalSettingsRepository;
            _userSettingsRepository = userSettingsRepository;
            _pageType = pageType;
        }

        public IEnumerable<ListItem> AvailablePageTypes()
        {
            var pageTypes = SelectablePageTypes().ToList();
            pageTypes.Insert(0, new ListItem
                                    {
                                        Text = _translator.Translate("/PageTypeTreeFilter/PageTypeSelector/ShowAllText"),
                                        Value = PageFilterConstants.ShowAllPageTypes
                                    });
            return pageTypes;
        }

        public IEnumerable<ListItem> SelectablePageTypes()
        {
            var globalSettings = _globalSettingsRepository.LoadGlobalSettings();
            if (!globalSettings.AllowUserSettings || !_userSettingsRepository.EnableUserSelectedPageTypes)
            {
                return CreateListItemsFor(globalSettings.SelectedPageTypeIds);
            }
            return CreateListItemsFor(_userSettingsRepository.UserSelectedPageTypeIds);
        }

        public IEnumerable<ListItem> CreateListItemsFor(string pageTypeIds)
        {
            if(string.IsNullOrEmpty(pageTypeIds))
            {
                return Enumerable.Empty<ListItem>();
            }
            return AllPageTypes().Where(t => pageTypeIds.Split(';').Contains(t.Value));
        }

        private IEnumerable<ListItem> AllPageTypes()
        {
            return _pageType.List().ToListItems();
        }
    }
}
