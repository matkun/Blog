using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Personalization;

namespace Lemonwhale.Core.Presentation.EPiServer.UserProfiles
{
    public class LemonwhaleSettingsUIPresenter : PresenterBase
    {
        // EPiServer.Personalization.EPiServerProfile.Current["foo"]
        // EPiServer.Personalization.EPiServerProfile.Current.Email
        private readonly ILemonwhaleSettingsUIPresenterView _view;

        public LemonwhaleSettingsUIPresenter(ILemonwhaleSettingsUIPresenterView view)
        {
            if (view == null) throw new ArgumentNullException("view");
            _view = view;

            //_view.LoadLwSettings += HandleLoadSettings;
            _view.SaveLwSettings += HandleSaveSettings;
        }

        public override void Load()
        {
            base.Load();
            _view.PrivateApiKeyLabel = "Private API key";
        }

        //private void HandleLoadSettings(object sender, LemonwhaleSettingsEventHandlerArgs args)
        //{
        //    // Only first time init
        //    _view.PrivateApiKey = args.Data[LemonwhaleSettingKeys.PrivateApiKey] as string;
        //}

        private void HandleSaveSettings(object sender, LemonwhaleSettingsEventHandlerArgs args)
        {
            args.Data[LemonwhaleSettingKeys.PrivateApiKey] = _view.PrivateApiKey;
            args.Data.Save();
        }
    }
}
