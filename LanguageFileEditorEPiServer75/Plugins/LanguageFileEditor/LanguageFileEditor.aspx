<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="LanguageFileEditor.aspx.cs" EnableViewState="true" Inherits="EPiServer.Templates.Alloy.Plugins.LanguageFileEditor.LanguageFileEditor" %>

<%@ Register TagPrefix="CodeSample" TagName="EditableXmlTree" Src="~/Plugins/LanguageFileEditor/EditableXmlTree.ascx" %>

<asp:content contentplaceholderid="HeaderContentRegion" runat="server">
	<link type="text/css" rel="stylesheet" media="all" href="/Plugins/LanguageFileEditor/LanguageFileEditor.css" />
	<script type="text/javascript" language="javascript" src="/Plugins/LanguageFileEditor/LanguageFileEditor.js"></script>
</asp:content>

<asp:content contentplaceholderid="MainRegion" runat="server">
	<div id="LanguageFileEditor">
		<span id="Message" class="message EP-systemMessage<%= ShowMessage ? " show" : string.Empty %>">
			<asp:Literal ID="litMessage" runat="server" />
		</span>
		<asp:MultiView ID="mvLanguageEditor" runat="server">

		<asp:View ID="vManageLanguageFiles" runat="server">
			
			 <asp:PlaceHolder ID="phReapplyChanges" Visible="false" runat="server">
				<fieldset>
					<legend>Reapply language file changes</legend>
					<label for="<%= ddlReapplyChanges.ClientID %>">Välj en fil med ändringar</label>
					<asp:DropDownList ID="ddlReapplyChanges" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
					<asp:Button ID="btnReapplyChanges" OnClick="btnReapplyChanges_OnClick" Text="Reapply" ToolTip="Reapply changes to this file." runat="server" />
					<asp:Button ID="btnReapplyAll" OnClick="btnReapplyAll_OnClick" Text="Reapply all" ToolTip="Reapply changes to all files. Changes made to removed language files will be skipped." runat="server" />
					<asp:Button ID="btnDeleteChanges" OnClick="btnDeleteChanges_OnClick" Text="Delete" ToolTip="Remove the possibility to reapply changes for this file." runat="server" />
				</fieldset>
			</asp:PlaceHolder>

			<asp:PlaceHolder ID="phRestoreBackups" Visible="false" runat="server">
				<fieldset>
					<legend>Manage backups</legend>
					<label for="<%= ddlRestoreBackup.ClientID %>">Select a file that you want to restore</label>
					<asp:DropDownList ID="ddlRestoreBackup" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
					<asp:Button ID="btnRestoreBackup" OnClick="btnRestoreBackup_OnClick" Text="Restore" ToolTip="Restore this file" runat="server" />
					<asp:Button ID="btnDeleteAllBackups" OnClick="btnDeleteAllBackups_OnClick" Text="Remove all backups" ToolTip="This removes all backups. If you click this button all of your backups will be lost. Forever." runat="server" />
				</fieldset>
			</asp:PlaceHolder>

			<fieldset>
				<legend>Manage available language files</legend>
				<asp:Repeater ID="rptLanguageFiles" runat="server">
					<HeaderTemplate>
						<table>
							<tr>
								<th>Edit file</th>
								<th>Backup file</th>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="row<%# AlternatingRowClass %>">
							<td>
								<asp:LinkButton
									ID="lbEditFile"
									CssClass="edit"
									CommandName="EditFile"
									CommandArgument="<%# Container.DataItem %>"
									OnCommand="lbEditFile_OnCommand"
									Text="<%# Container.DataItem %>"
									ToolTip="Click to edit this language file."
									runat="server"></asp:LinkButton>
							</td>
							<td>
								<asp:LinkButton
									ID="lbBackUpFile"
									CssClass="backUp"
									CommandName="BackUpFile"
									CommandArgument="<%# Container.DataItem %>"
									OnCommand="lbBackUpFile_OnCommand"
									Text="Back up"
									ToolTip="Click to back up this language file."
									runat="server"></asp:LinkButton>
							</td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>
			</fieldset>
		</asp:View>

			<asp:View ID="vEditLanguageFile" runat="server">
				<div id="ControlArea">
					<fieldset>
						<legend><asp:Literal ID="litEditLegend" runat="server"></asp:Literal></legend>
						<label for="<%= tbFileName.ClientID %>">Filename:</label>
						<asp:TextBox ID="tbFileName" CssClass="targetFilename" ValidationGroup="SaveFileValidationGroup" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator
							CssClass="errorMessage"
							ValidationGroup="SaveFileValidationGroup"
							ControlToValidate="tbFileName"
							ErrorMessage="Please name your file."
							runat="server"></asp:RequiredFieldValidator>
						<asp:HiddenField ID="hfPatternFilename" runat="server" />
						<input type="submit" class="saveFileButton" onclick="JavaScript: return false;" value="Save" />
						<asp:Button ID="btnCancel" OnClick="btnCancel_OnClick" Text="Cancel" CssClass="cancelButton show" runat="server" />
						<asp:Button ID="btnBack" OnClick="btnBack_OnClick" Text="Back to the list" ToolTip="Click here to return to the list of language files." CssClass="backButton hide" runat="server" />
					</fieldset>
				</div>
				<div id="EditArea">
					<fieldset>
						<CodeSample:EditableXmlTree ID="csEditableTree" runat="server" />
					</fieldset>
				</div>
			</asp:View>

		</asp:MultiView>
	</div>
</asp:content>
