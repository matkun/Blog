<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="LanguageFileEditor.aspx.cs" EnableViewState="true" Inherits="EPiServer.Plugins.LanguageFileEditor.LanguageFileEditor" %>
<%@ Register tagPrefix="CodeSample" TagName="EditableXmlTree" Src="~/Plugins/LanguageFileEditor/EditableXmlTree.ascx" %>

<asp:Content ContentPlaceHolderID="HeaderContentRegion" runat="server">
    <link type="text/css" rel="stylesheet" media="all" href="/Plugins/LanguageFileEditor/LanguageFileEditor.css" />
    <script type="text/javascript" language="javascript" src="/Plugins/LanguageFileEditor/LanguageFileEditor.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainRegion" runat="server">
    <div id="LanguageFileEditor">
        <span id="Message" class="message EP-systemMessage<%= ShowMessage ? " show" : string.Empty %>">
            <asp:Literal ID="litMessage" runat="server" />
        </span>
        <p>Tool for allowing EPiServer web administrators to create new, edit existing and remove old language files.</p>
        <asp:MultiView ID="mvLanguageEditor" runat="server">

            <asp:View ID="vManageLanguageFiles" runat="server">

                <fieldset>
                    <legend>Create new language file</legend>
                    <label for="<%= ddlCopyFrom.ClientID %>">Select existing file to use as a pattern</label>
                    <asp:DropDownList ID="ddlCopyFrom" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnNewFile" OnClick="btnNewFile_OnClick" Text="Create" ToolTip="Click to create a new language file from pattern." runat="server" />
                </fieldset>

                <asp:PlaceHolder ID="phReapplyChanges" Visible="false" runat="server">
                    <fieldset>
                        <legend>Reapply language file changes</legend>
                        <label for="<%= ddlReapplyChanges.ClientID %>">Select a file with changes</label>
                        <asp:DropDownList ID="ddlReapplyChanges" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnReapplyChanges" OnClick="btnReapplyChanges_OnClick" Text="Reapply" ToolTip="Reapply changes made to this file." runat="server" />
                        <asp:Button ID="btnReapplyAll" OnClick="btnReapplyAll_OnClick" Text="Reapply all" ToolTip="Reapply changes for all files. Changes made to a deleted language file vill be skipped." runat="server" />
                        <asp:Button ID="btnDeleteChanges" OnClick="btnDeleteChanges_OnClick" Text="Delete" ToolTip="Delete the possibility to reapply changes for this file." runat="server" />
                    </fieldset>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phRestoreBackups" Visible="false" runat="server">
                    <fieldset>
                        <legend>Manage backups</legend>
                        <label for="<%= ddlRestoreBackup.ClientID %>">Select the file that you want to restore</label>
                        <asp:DropDownList ID="ddlRestoreBackup" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnRestoreBackup" OnClick="btnRestoreBackup_OnClick" Text="Restore" ToolTip="Restore this file." runat="server" />
                        <asp:Button ID="btnDeleteAllBackups" OnClick="btnDeleteAllBackups_OnClick" Text="Delete all backups" ToolTip="Remove all backups. If you do this, all of your backups will be gone. Forever." runat="server" />
                    </fieldset>
                </asp:PlaceHolder>

                <fieldset>
                    <legend>Manage existing language files</legend>
                    <asp:Repeater ID="rptLanguageFiles" runat="server">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th>Edit file</th>
                                    <th>Back up file</th>
                                    <th>Delete file</th>
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
                                <td>
                                    <asp:LinkButton
                                        ID="lbDeleteFile"
                                        CssClass="delete"
                                        CommandName="BackUpFile"
                                        CommandArgument="<%# Container.DataItem %>"
                                        OnCommand="lbDeleteFile_OnCommand"
                                        Text="Delete"
                                        ToolTip="Click to delete this language file."
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
                    <asp:Button ID="btnBack" OnClick="btnBack_OnClick" Text="Return to file view" ToolTip="Click to return to the list of XML language files." CssClass="backButton hide" runat="server" />
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
</asp:Content>
