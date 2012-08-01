<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupFromUC.aspx.cs" Inherits="EPiServer.CodeSample.Popups.PopupFromUC" %>
<%@ Register TagPrefix="Sample" TagName="ImageSelectorPopupControl" src="ImageSelectorPopup.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title runat="server" />
    </head>
    <body id="body" runat="server">
        <form id="aspnetForm" runat="server">
            <h1>EPiServer Image Selector Pop-up from User Control</h1>

            <label for="<%= ImageSelector1.ClientID %>">First selector:</label>
            <Sample:ImageSelectorPopupControl ID="ImageSelector1" runat="server" />
            
            <label for="<%= ImageSelector2.ClientID %>">Second selector:</label>
            <Sample:ImageSelectorPopupControl ID="ImageSelector2" runat="server" />
            
            <label for="<%= ImageSelector3.ClientID %>">Third selector:</label>
            <Sample:ImageSelectorPopupControl ID="ImageSelector3" runat="server" />
        </form>
    </body>
</html>
