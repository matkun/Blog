<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LemonwhaleSettingsUI.ascx.cs" Inherits="Lemonwhale.Core.Framework.EPiServer.UserProfiles.LemonwhaleSettingsUI" %>

<div class="epi-padding" style="display: block;">
    <div style="margin: 0.5em 0;">
        <label for="<%= PersonalApiKeyTextBox.ClientID %>" style="display: inline-block; margin-right: 6px;" title="<%= PersonalApiKeyDescription %>"><%# PersonalApiKeyLabel %></label>
        <asp:TextBox CssClass="episize240" ID="PersonalApiKeyTextBox" runat="server" />
        <asp:CustomValidator
            ID="PersonalApiKeyValidator"
            ControlToValidate="PersonalApiKeyTextBox"
            OnServerValidate="ValidatePersonalApiKey_ServerValidate"
            runat="server"></asp:CustomValidator>
    </div>
</div>
