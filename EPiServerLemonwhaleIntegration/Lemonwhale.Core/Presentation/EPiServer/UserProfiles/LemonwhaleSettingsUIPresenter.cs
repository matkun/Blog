using System;
using Lemonwhale.Core.Framework.EPiServer.Language;

namespace Lemonwhale.Core.Presentation.EPiServer.UserProfiles
{
    public class LemonwhaleSettingsUIPresenter : PresenterBase
    {
        private readonly ITranslator _translator;
        private bool _firstTimeInit;
        // EPiServer.Personalization.EPiServerProfile.Current["foo"]
        // EPiServer.Personalization.EPiServerProfile.Current.Email
        private readonly ILemonwhaleSettingsUIPresenterView _view;

        //public LemonwhaleSettingsUIPresenter() {}

        public LemonwhaleSettingsUIPresenter(ILemonwhaleSettingsUIPresenterView view, ITranslator translator)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (translator == null) throw new ArgumentNullException("translator");
            _view = view;
            _translator = translator;

            _view.LoadLwSettings += HandleLoadSettings;
            _view.SaveLwSettings += HandleSaveSettings;
        }

        public override void FirstTimeInit()
        {
            base.FirstTimeInit();
            _firstTimeInit = true;
        }

        public override void Load()
        {
            base.Load();
            _view.PersonalApiKeyLabel = _translator.Translate("/Lemonwhale/UserSettings/PersonalApiKeyLabel");
            _view.PersonalApiKeyDescription = _translator.Translate("/Lemonwhale/UserSettings/PersonalApiKeyDescription");
            _view.PersonalApiKeyValidatorErrorMessage = _translator.Translate("/Lemonwhale/UserSettings/PersonalApiKeyValidatorErrorMessage");
        }

        private void HandleLoadSettings(object sender, LemonwhaleSettingsEventHandlerArgs args)
        {
            if(_firstTimeInit)
            {
                _view.PersonalApiKey = args.Data[LemonwhaleSettingKeys.PersonalApiKey] as string;
            }
        }

        private void HandleSaveSettings(object sender, LemonwhaleSettingsEventHandlerArgs args)
        {
            args.Data[LemonwhaleSettingKeys.PersonalApiKey] = _view.PersonalApiKey;
            args.Data.Save();
        }
    }
}
