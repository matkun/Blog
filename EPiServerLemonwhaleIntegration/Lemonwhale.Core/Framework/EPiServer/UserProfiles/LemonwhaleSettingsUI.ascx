<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LemonwhaleSettingsUI.ascx.cs" Inherits="Lemonwhale.Core.Framework.EPiServer.UserProfiles.LemonwhaleSettingsUI" %>

<div class="epi-padding" style="display: block;">
    <div style="margin: 0.5em 0;">
        <label for="<%= tbPrivateApiKey.ClientID %>" style="display: inline-block; margin-right: 6px;"><%# PrivateApiKeyLabel %></label>
        <asp:TextBox CssClass="episize240" ID="tbPrivateApiKey" runat="server" />
        <asp:CustomValidator
            ControlToValidate="tbPrivateApiKey"
            ErrorMessage="Private API key must be a valid Guid."
            OnServerValidate="ValidatePrivateApiKey_ServerValidate"
            runat="server"></asp:CustomValidator>
    </div>
</div>
