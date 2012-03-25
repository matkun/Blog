using System;
using EPiServer.UI;
using EPiServer.UI.WebControls;
using PageTypeTreeFilter.Framework.DataAccess.GlobalSettings;
using PageTypeTreeFilter.Framework.EmbeddedResources;
using PageTypeTreeFilter.Framework.Language;

namespace PageTypeTreeFilter.Presentation.Settings.Global
{
    public class GlobalSettingsPresenter : PresenterBase
    {
        private readonly IGlobalSettingsView _view;
        private readonly ITranslator _translator;
        private readonly IGlobalSettingsRepository _globalSettingsRepository;
        private readonly IResourceHandler _resourceHandler;

        public GlobalSettingsPresenter(
            IGlobalSettingsView view,
            ITranslator translator,
            IGlobalSettingsRepository globalSettingsRepository,
            IResourceHandler resourceHandler)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (translator == null) throw new ArgumentNullException("translator");
            if (globalSettingsRepository == null) throw new ArgumentNullException("globalSettingsRepository");
            if (resourceHandler == null) throw new ArgumentNullException("resourceHandler");
            _view = view;
            _translator = translator;
            _globalSettingsRepository = globalSettingsRepository;
            _resourceHandler = resourceHandler;

            _view.SaveGlobalSettings += SaveGlobalSettings;
            _view.ResetGlobalSettings += ResetGlobalSettings;
        }

        public override void PreInit()
        {
            base.PreInit();
            _view.CurrentPage.MasterPageFile = EPiServer.Configuration.Settings.Instance.UIUrl + "MasterPages/EPiServerUI.Master";
        }

        public override void Init()
        {
            base.Init();
            _resourceHandler.AddGlobalSettingsStyleSheet(_view.CurrentPage);
        }

        public override void Load()
        {
            base.Load();
            
            ((SystemPageBase)_view.CurrentPage).SystemMessageContainer.Heading = _translator.Translate("/PageTypeTreeFilter/GlobalSettings/Heading");
            _view.AllowUserSettingsLabel = _translator.Translate("/PageTypeTreeFilter/GlobalSettings/AllowUserSettingsLabel");
            _view.AllowUserSettingsDescription = _translator.Translate("/PageTypeTreeFilter/GlobalSettings/AllowUserSettingsDescription");
            _view.SaveButtonLabel = _translator.Translate("/PageTypeTreeFilter/GlobalSettings/SaveLabel");
            _view.SaveButtonDescription = _translator.Translate("/PageTypeTreeFilter/GlobalSettings/SaveDescription");
            _view.ResetButtonLabel = _translator.Translate("/PageTypeTreeFilter/GlobalSettings/ResetLabel");
            _view.ResetButtonDescription = _translator.Translate("/PageTypeTreeFilter/GlobalSettings/ResetDescription");
        }

        public override void PreRender()
        {
            base.PreRender();

            var settings = _globalSettingsRepository.LoadGlobalSettings();
            _view.AllowUserSettings = settings.AllowUserSettings;
            _view.SelectedGlobalPageTypeIds = settings.SelectedPageTypeIds;
        }

        private void SaveGlobalSettings(object sender, GlobalSettingsEventArgs e)
        {
            var newSettings = new GlobalSettingsDto
                                  {
                                      AllowUserSettings = e.AllowUserSettings,
                                      SelectedPageTypeIds = string.IsNullOrEmpty(e.SelectedPageTypeIds) ? string.Empty : e.SelectedPageTypeIds
                                  };
            _globalSettingsRepository.SaveGlobalSettings(newSettings);
            DisplaySystemMessage("/PageTypeTreeFilter/GlobalSettings/SaveSuccessfulMessage");
        }

        private void ResetGlobalSettings(object sender, GlobalSettingsEventArgs e)
        {
            _globalSettingsRepository.ResetGlobalSettings();
            DisplaySystemMessage("/PageTypeTreeFilter/GlobalSettings/ResetSuccessfulMessage");
        }

        private void DisplaySystemMessage(string xPath)
        {
            var page = (SystemPageBase)_view.CurrentPage;
            page.SystemMessageContainer.MessageStyle = SystemPrefix.MessageType.None;
            page.SystemMessageContainer.Message = _translator.Translate(xPath);
        }
    }
}
