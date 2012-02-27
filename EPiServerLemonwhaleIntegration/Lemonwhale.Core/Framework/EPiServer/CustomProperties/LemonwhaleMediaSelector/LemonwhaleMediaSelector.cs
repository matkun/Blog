using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using EPiServer.Core;
using EPiServer.Core.PropertySettings;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Web.PropertyControls;

namespace Lemonwhale.Core.Framework.EPiServer.CustomProperties.LemonwhaleMediaSelector
{
    [Serializable]
    [PageDefinitionTypePlugIn(
        DisplayName = "Lemonwhale media",
        Description = "Lemonwhale media files available.")]
    [PropertySettings(typeof(LemonwhaleMediaSelectorSettings), true)]
    public class LemonwhaleMediaSelector : PropertyString
    {
        public override IPropertyControl CreatePropertyControl()
        {
            return new LemonwhaleMediaSelectorControl();
        }
    }

    [PropertySettingsUI(AdminControl = typeof(LemonwhaleMediaSelectorSettingsUI))]
    public class LemonwhaleMediaSelectorSettings : PropertySettingsBase
    {
        public bool RequirePrivateApiKey { get; set; }

        public override IPropertySettings GetDefaultValues()
        {
            return new LemonwhaleMediaSelectorSettings { RequirePrivateApiKey = false };
        }
    }

    public class LemonwhaleMediaSelectorControl : PropertyTextBoxControlBase
    {
        protected DropDownList MediaSelector;
        public override bool SupportsOnPageEdit { get { return false; } }

        public override void CreateEditControls()
        {
            MediaSelector = new DropDownList();
            var settings = PropertyData.GetSetting(typeof(LemonwhaleMediaSelectorSettings)) as LemonwhaleMediaSelectorSettings;

            MediaSelector.Items.Add(new ListItem("-- No media selected --", "no-media-selected"));
            MediaSelector.Items.Add(new ListItem("Movie 1", "first-film"));
            MediaSelector.Items.Add(new ListItem("Movie 2", "second-film"));
            MediaSelector.Items.Add(new ListItem("Movie 3", "third-film"));
            MediaSelector.Items.Add(new ListItem("Requires private key: "+settings.RequirePrivateApiKey, "require-private-key"));

            ApplyControlAttributes(MediaSelector);
            Controls.Add(MediaSelector);
        }

        public override void ApplyEditChanges()
        {
            SetValue(MediaSelector.SelectedValue);
        }

        public LemonwhaleMediaSelector LemonwhaleMediaSelector
        {
            get { return PropertyData as LemonwhaleMediaSelector; }
        }
    }

    public class LemonwhaleMediaSelectorSettingsUI : PropertySettingsControlBase
    {
        private CheckBox _requirePrivateApiKey;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            _requirePrivateApiKey = new CheckBox { ID = "RequirePrivateApiKey", Enabled = true};
            var requireLabel = new Label
                                   {
                                       Text = "Require private API key",
                                       AssociatedControlID = "RequirePrivateApiKey"
                                   };
            Controls.Add(requireLabel);
            Controls.Add(_requirePrivateApiKey);
        }

        public override void LoadSettingsUI(IPropertySettings propertySettings)
        {
            EnsureChildControls();
            _requirePrivateApiKey.Checked = ((LemonwhaleMediaSelectorSettings)propertySettings).RequirePrivateApiKey;
        }

        public override void UpdateSettings(IPropertySettings propertySettings)
        {
            EnsureChildControls();
            ((LemonwhaleMediaSelectorSettings)propertySettings).RequirePrivateApiKey = _requirePrivateApiKey.Checked;
        }
    }
}
