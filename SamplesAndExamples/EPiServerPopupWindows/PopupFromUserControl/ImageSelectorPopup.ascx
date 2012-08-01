<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ImageSelectorPopup.ascx.cs" Inherits="EPiServer.CodeSample.Popups.ImageSelectorPopup" %>

<div style="margin: 10px;">
    <label for="image-selector-<%= UniqueIdentifier %>">Select a file:</label>
    <input id="image-selector-<%= UniqueIdentifier %>" type="text" maxlength="255" name="image-selector-<%= UniqueIdentifier %>" />
    <input id="image-selector-button-<%= UniqueIdentifier %>" type="button" value="..." name="image-selector-button-<%= UniqueIdentifier %>" />
</div>

<script type="text/javascript" language="javascript">
    // <![CDATA[
    $(document).ready(function () {
        $('document').ready(function () {
            var selector = ImageSelector.Popup('<%= UniqueIdentifier %>');
            selector.init();
        });
    });
    // ]]>
</script>
