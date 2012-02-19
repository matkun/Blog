<%@ Page language="C#" CodeBehind="StaticError.aspx.cs" Inherits="EPiServer.CodeSample.StaticError" MasterPageFile="~/CodeSample/StaticMaster.Master" AutoEventWireup="false" EnableViewState="false" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <link href="style.css" media="all" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <EPiServer:Property runat="server" PropertyName="ErrorMessage" />
</asp:Content>