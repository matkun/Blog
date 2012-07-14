<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="UrlSelectorPopup.aspx.cs" Inherits="EPiServer.CodeSample.Popups.UrlSelectorPopup" MasterPageFile="PopupDemo.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="FullRegion">
    <div style="margin: 10px;">
        <input id="url-selector" class="episize240" type="text" maxlength="255" value="/default.aspx?id=3" name="url-selector">
        <input class="epismallbutton" type="button" onclick="PropertyUrl_DoPickup('url-selector',130);" value="..." name="url-selector-button">
    </div>
    
    <script type='text/javascript'>
    // <![CDATA[
        function PropertyUrl_DoPickup(obj, type) {
            var node = document.getElementById(obj);
            var browserselectionmode = '';
            var callbackMethod;
            var fileManagerBaseUrl = EPi.ResolveUrlFromUI("edit/FileManagerBrowser.aspx");
            if (type == 128) {
                browserselectionmode = '&browserselectionmode=image';
            }
            else if (type == 129) {
                browserselectionmode = '&browserselectionmode=file';
            }
            if (browserselectionmode != '') {
                callbackMethod = function(returnValue, callbackArgs) {
                    if (returnValue) {
                        if (returnValue.closeAction == 'clear') {
                            callbackArgs.value = '';
                        } else if (returnValue.items && returnValue.items.length == 1) {
                            callbackArgs.value = returnValue.items[0].path;
                        }
                        EPi.DispatchEvent(callbackArgs, 'change');
                    }
                }; // TODO - You might want not want to use these static id values in your code.
                var dialogUrl = fileManagerBaseUrl + '?id=3_362&parentid=1&pagefolderid=3' + browserselectionmode + '&selectedfile=' + encodeURIComponent(node.value);
                EPi.CreateDialog(dialogUrl, callbackMethod, node, null, { width: 600 });
            }
            else {
                var linkObject = new Object();
                if (node && node.value.length > 0) {
                    var href = node.value;
                    linkObject.href = href;
                }
                linkObject.onlyUrl = true; // TODO - You might want not want to use these static id values in your code.
                linkObject.fileManagerBrowserUrl = fileManagerBaseUrl + '?id=3_362&parentid=1&pagefolderid=3';
                callbackMethod = function(returnValue, callbackArgs) {
                    if (returnValue) {
                        if (returnValue == -1) {
                            callbackArgs.value = '';
                        } else if (returnValue != 0) {
                            callbackArgs.value = returnValue.href;
                        }
                        EPi.DispatchEvent(callbackArgs, 'change');
                    }
                };
                var baseUrl = EPi.ResolveUrlFromUI("editor/dialogs/HyperlinkProperties.aspx");
                EPi.CreateDialog(baseUrl + '?url=' + encodeURIComponent(node.value), callbackMethod, node, linkObject, { width: 460, height: 260, scrollbars: 'no' });
            }
        }
    // ]]>
    </script>
</asp:Content>
