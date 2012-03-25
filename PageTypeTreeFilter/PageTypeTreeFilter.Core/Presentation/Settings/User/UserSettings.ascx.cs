using System;
using EPiServer.Personalization;
using EPiServer.PlugIn;

namespace PageTypeTreeFilter.Presentation.Settings.User
{
    [GuiPlugIn(
        Area = PlugInArea.SidSettingsArea,
        LanguagePath = "/PageTypeTreeFilter/UserSettings/GuiPlugIn",
        Url = "~/PageTypeTreeFilterResource/PageTypeTreeFilter.dll/PageTypeTreeFilter.Presentation.Settings.User.UserSettings.ascx")]
    public partial class UserSettings : PresentedControlBase<UserSettingsPresenter>, IUserSettingsView, IUserSettings, ICustomPlugInLoader
    {
        public bool SaveRequiresUIReload { get; set; }
        public bool AllowUserSettings { get; set; }

        public event UserSettingsEventHandler SaveUserSettings;
        public event UserSettingsEventHandler LoadUserSettings;
        
        public string SelectedPageTypeIds
        {
            get { return ((PageTypePicker)ucPageTypePicker).SelectedPageTypeIds; }
            set { ((PageTypePicker)ucPageTypePicker).SelectedPageTypeIds = value; }
        }

        public string UserSettingsDisabledMessage
        {
            get { return lblUserSettingsDisabledMessage.Text; }
            set { lblUserSettingsDisabledMessage.Text = value; }
        }

        public string EnablePersonalPageTypesLabel
        {
            get { return cbEnableUserSpecificPageTypes.Text; }
            set { cbEnableUserSpecificPageTypes.Text = value; }
        }

        public string EnablePersonalPageTypesDescription
        {
            get { return cbEnableUserSpecificPageTypes.ToolTip; }
            set { cbEnableUserSpecificPageTypes.ToolTip = value; }
        }

        public bool EnablePersonalPageTypes
        {
            get { return cbEnableUserSpecificPageTypes.Checked; }
            set { cbEnableUserSpecificPageTypes.Checked = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataBindChildren();
        }
        
        public void LoadSettings(string userName, EPiServerProfile data)
        {
            if (LoadUserSettings != null)
            {
                LoadUserSettings(this, new UserSettingsEventHandlerArgs(userName, data));
            }
        }

        public void SaveSettings(string userName, EPiServerProfile data)
        {
            if (SaveUserSettings != null)
            {
                SaveUserSettings(this, new UserSettingsEventHandlerArgs(userName, data));
            }
        }

        public PlugInDescriptor[] List()
        {
            return new[] { PlugInDescriptor.Load(typeof(UserSettings)) };
        }
    }
}
