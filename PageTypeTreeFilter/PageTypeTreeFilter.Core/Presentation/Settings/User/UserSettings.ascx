<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="UserSettings.ascx.cs" Inherits="PageTypeTreeFilter.Presentation.Settings.User.UserSettings" %>
<%@ Register TagPrefix="uc" TagName="PageTypePicker" Src="~/PageTypeTreeFilterResource/PageTypeTreeFilter.dll/PageTypeTreeFilter.Presentation.Settings.PageTypePicker.ascx" %>

<div id="page-type-filter-user-settings" class="epi-padding">
    <div id="user-settings-container">
        <asp:Label
            ID="lblUserSettingsDisabledMessage"
            CssClass="disabled-message"
            Visible="<%# !AllowUserSettings %>"
            runat="server"></asp:Label>
        <asp:CheckBox ID="cbEnableUserSpecificPageTypes" runat="server" />
        <uc:PageTypePicker id="ucPageTypePicker" Enabled="False" runat="server" />
    </div>
</div>

<asp:PlaceHolder Visible="<%# AllowUserSettings %>" runat="server">
<script type="text/javascript" language="javascript">
//<![CDATA[
    var UserSettings = {
        initialize: function () {
            $('#<%= cbEnableUserSpecificPageTypes.ClientID %>').removeAttr("disabled");
            $('#<%= cbEnableUserSpecificPageTypes.ClientID %>').bind('click', UserSettings.TogglePickerBoxes);
            UserSettings.TogglePickerBoxes();
        },

        TogglePickerBoxes: function () {
            if ($('#<%= cbEnableUserSpecificPageTypes.ClientID %>').is(':checked')) {
                UserSettings.EnableBoxes();
            } else {
                UserSettings.DisableBoxes();
            }
        },

        DisableBoxes: function () {
            $('#available').attr("disabled", "disabled");
            $('#selected').attr("disabled", "disabled");
            $('#add-page-type-button').attr("disabled", "disabled");
            $('#remove-page-type-button').attr("disabled", "disabled");
        },

        EnableBoxes: function () {
            $('#available').removeAttr("disabled");
            $('#selected').removeAttr("disabled");
            $('#add-page-type-button').removeAttr("disabled");
            $('#remove-page-type-button').removeAttr("disabled");
        }
    };

    $(document).ready(function () {
        UserSettings.initialize();
    });
//]]>
</script>
</asp:PlaceHolder>

<asp:PlaceHolder Visible="<%# !AllowUserSettings %>" runat="server">
<script type="text/javascript" language="javascript">
//<![CDATA[
    $(document).ready(function () {
        $('#<%= cbEnableUserSpecificPageTypes.ClientID %>').attr("disabled", "disabled");
    });
//]]>
</script>
</asp:PlaceHolder>
