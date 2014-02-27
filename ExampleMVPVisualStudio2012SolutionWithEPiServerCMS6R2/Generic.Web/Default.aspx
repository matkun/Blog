<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeBehind="Default.aspx.cs" Inherits="Generic.Web.Default" %>
<%@ Register TagPrefix="uc" TagName="ControlText" src="Templates/Units/StartPage/SampleControl.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2><asp:Literal ID="litImportantHeading" runat="server"></asp:Literal></h2>
    <p><asp:Literal ID="litImportantText" runat="server"></asp:Literal></p>
    <p><uc:ControlText ID="ucControlText" runat="server" /></p>
</asp:Content>
