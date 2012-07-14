<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="XFormSelectorPopup.aspx.cs" Inherits="EPiServer.CodeSample.Popups.XFormSelectorPopup" MasterPageFile="PopupDemo.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="FullRegion">
    <div style="margin: 10px;"> <%-- Sample 'value':s in _Value and _Display input-fields, as well as the pageId (3_362) and parentId (1) in the onBrowseXForm call below. --%>
        <input id="xform-selector_Value" type="hidden" value="d883ef9b-88de-43e2-83ed-dd3d08e50215" name="xform-selector_Value">
        <input id="xform-selector_Display" class="episize240" type="text" disabled="disabled" size="30" value="Contact" name="xform-selector_Display">
        <input id="xform-selector-button" class="epismallbutton" type="button" onclick="return onBrowseXForm(document.getElementById('xform-selector_Value').value,'xform-selector_Display','xform-selector_Value','3_362','1')" value="..." name="xform-selector-button">
    </div>
    
    <script type='text/javascript'>
    //<![CDATA[
        function onBrowseXForm(id, s1, s2, pageId, parentId) {
            // Open a new dialog. Pass s1 and s2 as objectProperties to dialogWindow. These will be returned in the returnValue passed to OnXFormBrowseCompleted in the XFormSelect close dialog method.
            var dialogWidth = 920;
            var dialogHeight = 565;
            var baseUrl = EPi.ResolveUrlFromUI("edit/XFormSelect.aspx");
            EPi.CreateDialog(baseUrl + '?form=' + id + '&pageId=' + pageId + '&parentId=' + parentId, OnXFormBrowseCompleted, { s1: s1, s2: s2 }, null, { width: dialogWidth, height: dialogHeight });
            return false;
        }
        function OnXFormBrowseCompleted(objectReturned, onCompleteArguments) {
            // objectReturned is passed as a returnValue of the dialog opened by onBrowseXForm
            // onCompleteArguments is a context object created by onBrowseXForm and should include properties s1, s2.
            if (!objectReturned && objectReturned != '') {
                return;
            }
            var s1 = document.getElementById(onCompleteArguments.s1);
            var s2 = document.getElementById(onCompleteArguments.s2);
            if (!s1 || !s2) {
                alert('Xform selection failed');
                return;
            }
            EPi.PageLeaveCheck.SetPageChanged(true);
            if (objectReturned.id) {
                s1.value = objectReturned.name;
                s2.value = objectReturned.id;
            }
            else {
                s1.value = '';
                s2.value = '';
            }
        }
    //]]>
    </script>
</asp:Content>
