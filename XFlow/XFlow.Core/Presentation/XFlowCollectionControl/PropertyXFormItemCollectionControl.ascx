<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PropertyXFormItemCollectionControl.ascx.cs" Inherits="XFlow.Core.Presentation.XFlowCollectionControl.PropertyXFormItemCollectionControl" %>

<div id="xform-item-collection-<%= UniqueIdentifier %>">
    <div class="epi-buttonDefault">
        <span class="epi-cmsButton">
            <input type="button" 
                   id="new-xform-item-button-<%= UniqueIdentifier %>"
                   onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)" 
                   onmouseover="EPi.ToolButton.MouseDownHandler(this)" 
                   value="Lägg till"
                   name="addXFormItem"
                   class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Add">
        </span>
       
    </div>

    <table class="xform-item-table epi-default">
        <tr>
            <th colspan="2"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, edit.linkcollection.sortheading %>" /></th>
            <th><asp:Literal runat="server" Text="<%$ Resources: EPiServer, XFlow.EditModeProperty.Form %>" /></th>
            <th style="text-align: center;"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, button.edit %>" /></th>
            <th style="text-align: center;"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, button.delete %>" /></th>
        </tr>
    </table>
</div>

<script type="text/javascript" language="javascript">
// <![CDATA[
    $(document).ready(function () {
        $('document').ready(function () {
            var xFormEditor = XFlow.EditModeXFormCollection('<%= UniqueIdentifier %>', '<%= XFormEditorUrl %>', '<%= InitialXFormItems %>', eval('<%= EmptyXFormItem %>')[0]);
            xFormEditor.init();
            $("#new-xform-item-button-<%= UniqueIdentifier %>").click(xFormEditor.newItem);
        });
    });
// ]]>
</script>
