<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditableXmlNode.ascx.cs" Inherits="EPiServer.Templates.Alloy.Plugins.LanguageFileEditor.EditableXmlNode" %>
<%@ Import Namespace="System.Xml" %>
<%@ Register TagPrefix="LFE" TagName="EditableXmlAttributes" Src="~/Plugins/LanguageFileEditor/EditableXmlAttributes.ascx" %>

<asp:Repeater ID="rptXmlNodeList" OnItemDataBound="rptXmlNodeList_OnItemDataBound" runat="server">
	<HeaderTemplate>
		<ul>
	</HeaderTemplate>
	<ItemTemplate>
		<li>
			<div class="nodeStart">
				<asp:PlaceHolder ID="phToggleLink" runat="server"></asp:PlaceHolder>
				<strong>&lt;<%# Container.DataItem != null ? ((XmlNode)Container.DataItem).Name : string.Empty %></strong><LFE:EditableXmlAttributes DataSource="<%# Container.DataItem != null ? ((XmlNode)Container.DataItem).Attributes : null %>" runat="server" /><strong>&gt;</strong>
			</div>
			<div class="toggleBlock">
				<asp:PlaceHolder ID="phContent" runat="server"></asp:PlaceHolder>
				<strong>&lt;/<%# Container.DataItem != null ? ((XmlNode)Container.DataItem).Name : string.Empty %>&gt;</strong>
			</div>
		</li>
	</ItemTemplate>
	<FooterTemplate>
		</ul>
	</FooterTemplate>
</asp:Repeater>
