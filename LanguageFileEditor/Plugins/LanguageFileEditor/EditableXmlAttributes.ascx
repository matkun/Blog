<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="EditableXmlAttributes.ascx.cs" Inherits="EPiServer.Plugins.LanguageFileEditor.EditableXmlAttributes" %>
<%@ Import Namespace="System.Xml" %>

<asp:Repeater ID="rptAttributes" OnItemDataBound="rptAttributes_OnItemDataBound" runat="server">
    <ItemTemplate>
        <div class="attribute">
            <%# Container.DataItem != null ? ((XmlAttribute)Container.DataItem).Name : string.Empty %>=
            <asp:PlaceHolder ID="phTextBox" runat="server"></asp:PlaceHolder>
        </div>
    </ItemTemplate>
</asp:Repeater>