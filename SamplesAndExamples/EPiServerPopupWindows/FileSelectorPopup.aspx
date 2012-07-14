<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="FileSelectorPopup.aspx.cs" Inherits="EPiServer.CodeSample.Popups.FileSelectorPopup" MasterPageFile="PopupDemo.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="FullRegion">
    <div style="margin: 10px;">
        <label for="image-selector">Select a file:</label>
        <input id="image-selector" class="episize240" type="text" maxlength="255" name="image-selector" />
        <input id="image-selector-button" class="epismallbutton" type="button" value="..." name="image-selector-button" />
    </div>
    
    <script language="javascript" type="text/javascript">
    // <![CDATA[
        function epiFileManagerDialog(selectedFile, onCloseCallbackFunction) {
            var onCompleteArguments = new Object();
            var dialogArguments = new Object();
            dialogArguments.hideBookmarks = true;
            dialogArguments.parentWindow = document.window;
            var fileManagerBrowserUrl = EPi.ResolveUrlFromUI("edit/FileManagerBrowser.aspx") + "?selectedfile=" + selectedFile;
            EPi.CreateDialog(fileManagerBrowserUrl, onCloseCallbackFunction, onCompleteArguments, dialogArguments, { width: 660, height: 460, scrollbars: "no" });
        };
        function selectFile(evt, urlInputField) {
            epiFileManagerDialog(urlInputField.val(), function (a, b) {
                if (a.closeAction == 'insert') {
                    var selectedFilePath = a.items[0].path;
                    urlInputField.val(selectedFilePath);
                    EPi.PageLeaveCheck.SetPageChanged(true);
                } else if (a.closeAction == 'clear') {
                    $("#image-selector").val('');
                    EPi.PageLeaveCheck.SetPageChanged(true);
                } else if (a.closeAction == 'cancel') {
                    // ..
                }
            });
        };
        function initialize() {
            var urlInputField = $("#image-selector");
            $("#image-selector-button").click(function (evt) { selectFile(evt, urlInputField); });
        }
        $(document).ready(initialize);
    // ]]>
    </script>
</asp:Content>
