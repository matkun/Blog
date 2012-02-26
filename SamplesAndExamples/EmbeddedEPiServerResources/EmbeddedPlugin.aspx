<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="EmbeddedPlugin.aspx.cs" EnableViewState="true" Inherits="EmbeddedEPiServerResources.EmbeddedPlugin" %>
<%@ Register TagPrefix="uc" TagName="EmbeddedControl" Src="~/EmbeddedResource/EmbeddedEPiServerResources.dll/EmbeddedEPiServerResources.EmbeddedControl.ascx" %>

<asp:Content ContentPlaceHolderID="MainRegion" runat="server">
    <p>An embedded EPiServer plugin.</p>
    <p>This text is written in the embedded plugin page file.</p>
    <uc:EmbeddedControl runat="server" />
    <p><a href="/EmbeddedResource/EmbeddedEPiServerResources.dll/EmbeddedEPiServerResources.EmbeddedPlugin.aspx?param1=value1&param2=value2&param3=value3" title="This link makes use of query string parameters.">This link passes query string parameters to the embedded aspx file.</a></p>
    <asp:PlaceHolder ID="QueryParameterExample" runat="server"></asp:PlaceHolder>
</asp:Content>
