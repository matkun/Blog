using System;

namespace PageTypeTreeFilter.Presentation.Settings.Global
{
    public partial class GlobalSettings : PresentedSystemPageBase<GlobalSettingsPresenter>, IGlobalSettingsView
    {
        public event GlobalSettingsEventHandler SaveGlobalSettings;
        public event GlobalSettingsEventHandler ResetGlobalSettings;

        public string SelectedGlobalPageTypeIds
        {
            get { return ((PageTypePicker)ucPageTypePicker).SelectedPageTypeIds; }
            set { ((PageTypePicker)ucPageTypePicker).SelectedPageTypeIds = value; }
        }
        
        public string SaveButtonLabel
        {
            get { return btnSave.Text; }
            set { btnSave.Text = value; }
        }

        public string SaveButtonDescription
        {
            get { return btnSave.ToolTip; }
            set { btnSave.ToolTip = value; }
        }
        
        public string ResetButtonLabel
        {
            get { return btnResetToDefault.Text; }
            set { btnResetToDefault.Text = value; }
        }

        public string ResetButtonDescription
        {
            get { return btnResetToDefault.ToolTip; }
            set { btnResetToDefault.ToolTip = value; }
        }

        public string AllowUserSettingsLabel
        {
            get { return cbAllowUserSettings.Text; }
            set { cbAllowUserSettings.Text = value; }
        }

        public string AllowUserSettingsDescription
        {
            get { return cbAllowUserSettings.ToolTip; }
            set { cbAllowUserSettings.ToolTip = value; }
        }

        public bool AllowUserSettings
        {
            get { return cbAllowUserSettings.Checked; }
            set { cbAllowUserSettings.Checked = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataBindChildren();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(SaveGlobalSettings != null)
            {
                var args = new GlobalSettingsEventArgs
                               {
                                   AllowUserSettings = AllowUserSettings,
                                   SelectedPageTypeIds = SelectedGlobalPageTypeIds
                               };
                SaveGlobalSettings(sender, args);
            }
        }
        
        protected void btnResetToDefault_Click(object sender, EventArgs e)
        {
            if (ResetGlobalSettings != null)
            {
                ResetGlobalSettings(null, null);
            }
        }
    }
}
