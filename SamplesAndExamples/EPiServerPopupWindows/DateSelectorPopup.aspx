<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="DateSelectorPopup.aspx.cs" Inherits="EPiServer.CodeSample.Popups.DateSelectorPopup" MasterPageFile="PopupDemo.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="FullRegion">
    <div style="margin: 10px;">
        <input id="datetime-selector" class="episize240" type="text" size="20" maxlength="20" name="datetime-selector">
        <input class="epismallbutton" type="button" onclick="EPi.CreateDateBrowserDialog('/util/DateBrowser.aspx', 'datetime-selector');" value="..." name="datetime-selector-button">
    </div>
    <%--<EPiServer:InputDate runat="server"/>--%>
</asp:Content>
