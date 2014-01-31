<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditableXmlTree.ascx.cs" Inherits="EPiServer.Plugins.LanguageFileEditor.EditableXmlTree" %>
<%@ Register TagPrefix="LFE" TagName="EditableXmlNode" Src="~/Plugins/LanguageFileEditor/EditableXmlNode.ascx" %>

<asp:PlaceHolder ID="phXmlDeclaration" runat="server">
    <ul>
        <li class="xmlDeclaration">
            <%--Web editors / administrators have no business changing the XMl declaration.--%>
            <strong>&lt;?xml</strong> version="<%= XmlDeclaration.Version %>" encoding="<%= XmlDeclaration.Encoding %>" standalone="<%= XmlDeclaration.Standalone %>" <strong>?&gt;</strong>
        </li>
    </ul>
</asp:PlaceHolder>
<LFE:EditableXmlNode ID="csNodes" runat="server" />
