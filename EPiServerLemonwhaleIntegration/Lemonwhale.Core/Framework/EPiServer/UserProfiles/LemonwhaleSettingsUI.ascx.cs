using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer.Personalization;
using EPiServer.PlugIn;
using Lemonwhale.Core.Presentation;
using Lemonwhale.Core.Presentation.EPiServer.UserProfiles;

namespace Lemonwhale.Core.Framework.EPiServer.UserProfiles
{
    [GuiPlugIn(
        DisplayName = "Lemonwhale",
        Description = "User specific Lemonwhale settings",
        Area = PlugInArea.SidSettingsArea,
        Url = "~/LemonwhaleResource/Lemonwhale.Core.dll/Lemonwhale.Core.Framework.EPiServer.UserProfiles.LemonwhaleSettingsUI.ascx")]
    public partial class LemonwhaleSettingsUI : PresentedControlBase<LemonwhaleSettingsUIPresenter>, ILemonwhaleSettingsUIPresenterView, IUserSettings, ICustomPlugInLoader
    {
        public bool SaveRequiresUIReload { get; set; }

        public event LemonwhaleSettingsEventHandler SaveLwSettings;
       public event LemonwhaleSettingsEventHandler LoadLwSettings;
        

        public string PrivateApiKeyLabel { get; set; }

        public string PrivateApiKey
        {
            get { return tbPrivateApiKey.Text; }
            set { tbPrivateApiKey.Text = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataBindChildren();
        }

        public void LoadSettings(string userName, EPiServerProfile data)
        {
            //if(!IsPostBack)
            //{
            //    PrivateApiKey = data[LemonwhaleSettingKeys.PrivateApiKey] as string;
            //}
            if (LoadLwSettings != null)
            {
                LoadLwSettings(this, new LemonwhaleSettingsEventHandlerArgs(userName, data));
            }
        }

        public void SaveSettings(string userName, EPiServerProfile data)
        {
            if (SaveLwSettings != null)
            {
                SaveLwSettings(this, new LemonwhaleSettingsEventHandlerArgs(userName, data));
            }
        }

        public PlugInDescriptor[] List()
        {
            return new[] { PlugInDescriptor.Load(typeof(LemonwhaleSettingsUI)) };
        }

        protected void ValidatePrivateApiKey_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Guid temp;
            args.IsValid = Guid.TryParse(args.Value, out temp);
        }
    }
}
