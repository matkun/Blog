using System;
using System.Web;
using PageTypeTreeFilter.Framework.DataAccess.GlobalSettings;
using PageTypeTreeFilter.Framework.EmbeddedResources;
using PageTypeTreeFilter.Framework.Language;
using PageTypeTreeFilter.Framework.Logging;
using log4net;

namespace PageTypeTreeFilter.Presentation.Settings.User
{
    public class UserSettingsPresenter : PresenterBase
    {
        private readonly ITranslator _translator;
        private readonly IGlobalSettingsRepository _globalSettingsRepository;
        private readonly IResourceHandler _resourceHandler;
        private readonly HttpContextBase _context;
        private readonly IUserSettingsView _view;
        private readonly ILog _log;
        private bool _firstTimeInit;

        public UserSettingsPresenter(
            IUserSettingsView view,
            ITranslator translator,
            IGlobalSettingsRepository globalSettingsRepository,
            IResourceHandler resourceHandler,
            HttpContextBase context)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (translator == null) throw new ArgumentNullException("translator");
            if (globalSettingsRepository == null) throw new ArgumentNullException("globalSettingsRepository");
            if (resourceHandler == null) throw new ArgumentNullException("resourceHandler");
            if (context == null) throw new ArgumentNullException("context");
            _view = view;
            _translator = translator;
            _globalSettingsRepository = globalSettingsRepository;
            _resourceHandler = resourceHandler;
            _context = context;
            _log = Log.For(this);

            _view.LoadUserSettings += HandleLoadSettings;
            _view.SaveUserSettings += HandleSaveSettings;
        }

        public override void FirstTimeInit()
        {
            base.FirstTimeInit();
            _firstTimeInit = true;
        }

        public override void Init()
        {
            base.Init();
            _resourceHandler.AddUserSettingsStyleSheet(_view.CurrentPage);
        }

        public override void Load()
        {
            base.Load();
            _view.EnablePersonalPageTypesLabel = _translator.Translate("/PageTypeTreeFilter/UserSettings/EnablePersonalSelectionLabel");
            _view.EnablePersonalPageTypesDescription = _translator.Translate("/PageTypeTreeFilter/UserSettings/EnablePersonalSelectionDescription");
            _view.UserSettingsDisabledMessage = _translator.Translate("/PageTypeTreeFilter/UserSettings/UserSettingsDisabledMessage");

            var globalSettings = _globalSettingsRepository.LoadGlobalSettings();
            _view.AllowUserSettings = globalSettings.AllowUserSettings;
        }

        private void HandleLoadSettings(object sender, UserSettingsEventHandlerArgs args)
        {
            if (_firstTimeInit)
            {
                _view.EnablePersonalPageTypes = (bool)args.Data.GetPropertyValue(UserSettingsKeys.EnableUserSelectedPageTypes);
                _view.SelectedPageTypeIds = args.Data[UserSettingsKeys.UserSelectedPageTypes] as string;
            }
        }

        private void HandleSaveSettings(object sender, UserSettingsEventHandlerArgs args)
        {
            args.Data[UserSettingsKeys.EnableUserSelectedPageTypes] = _view.EnablePersonalPageTypes;
            args.Data[UserSettingsKeys.UserSelectedPageTypes] = _view.SelectedPageTypeIds;
            args.Data.Save();
            _log.InfoFormat(
                "User '{0}' saved user specific PageTypeTreeFilter settings (EnableUserSelectedPageTypes:'{1}', UserSelectedPageTypes:'{2}').",
                _context.User.Identity.Name, _view.EnablePersonalPageTypes, _view.SelectedPageTypeIds);
        }
    }
}
