using System;
using System.Web.UI;
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

        public event LemonwhaleSettingsEventHandler Save;
        public event LemonwhaleSettingsEventHandler Load;

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
            if(Load != null)
            {
                Load(this, new LemonwhaleSettingsEventHandlerArgs(userName, data));
            }
        }

        public void SaveSettings(string userName, EPiServerProfile data)
        {
            if (Save != null)
            {
                Save(this, new LemonwhaleSettingsEventHandlerArgs(userName, data));
            }
        }

        public PlugInDescriptor[] List()
        {
            return new[] { PlugInDescriptor.Load(typeof(LemonwhaleSettingsUI)) };
        }
    }
}
