<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="PageSelectorPopup.aspx.cs" Inherits="EPiServer.CodeSample.Popups.PageSelectorPopup" MasterPageFile="PopupDemo.Master" %>
<%--<%@ Register TagPrefix="EPiServer" Assembly="EpiServer" Namespace="EPiServer.Web.WebControls" %> Or in configuration file. --%>

<asp:Content runat="server" ContentPlaceHolderID="FullRegion">
    <div style="margin: 10px;">
        <label for="page-selector_Display">Select a page:</label>
        <input type="hidden" id="page-selector_Value" name="page-selector_Value" />
        <input type="text" id="page-selector_Display" name="page-selector_Display" class="episize240" disabled="disabled" />

        <input id="page-selector-button" name="page-selector-button" class="epismallbutton" type="button" onclick="EPi.CreatePageBrowserDialog('/secure/ui/CMS/edit/pagebrowser.aspx',document.getElementById('page-selector_Value').value,'False','False','page-selector_Display','page-selector_Value','en', null, null, false);" value="..." />
    </div>
    
    <div style="margin: 10px;">
        Select a page: <EPiServer:InputPageReference ID="DemoReference" runat="server" />
    </div>
</asp:Content>
