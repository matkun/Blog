<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="GlobalSettings.aspx.cs" Inherits="PageTypeTreeFilter.Presentation.Settings.Global.GlobalSettings" %>
<%@ Register TagPrefix="uc" TagName="PageTypePicker" Src="~/PageTypeTreeFilterResource/PageTypeTreeFilter.dll/PageTypeTreeFilter.Presentation.Settings.PageTypePicker.ascx" %>

<asp:Content ContentPlaceHolderID="MainRegion" runat="server">
    <div id="page-type-filter-global-settings" class="epi-padding">
        <div id="global-settings-container">
            <asp:CheckBox ID="cbAllowUserSettings" runat="server" />
            <uc:PageTypePicker id="ucPageTypePicker" Enabled="True" runat="server" />
        </div>
    </div>
    <div id="button-container" class="epi-buttonContainer">
        <span class="epi-cmsButton">
            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" />
        </span>
        <span class="epi-cmsButton">
            <asp:Button ID="btnResetToDefault" OnClick="btnResetToDefault_Click" runat="server" />
        </span>
    </div>
</asp:Content>
