<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeBehind="ImageSlideEditor.aspx.cs" Inherits="ImageSlideShow.Core.Presentation.ImageSlideShowControl.ImageSlideEditor" %>

<asp:Content ContentPlaceHolderID="FullRegion" runat="server">
    <script type="text/javascript">
    // <![CDATA[
        var imageSlide;
        function epiFileManagerDialog(selectedFile, onClose) {
            var onCompleteArguments = new Object();
            var dialogArguments = new Object();
            dialogArguments.hideBookmarks = true;
            dialogArguments.parentWindow = document.window;
            var fileManagerBrowserUrl = EPi.ResolveUrlFromUI("edit/FileManagerBrowser.aspx") + "?selectedfile=" + selectedFile;
            EPi.CreateDialog(fileManagerBrowserUrl, onClose, onCompleteArguments, dialogArguments, { width: 660, height: 460, scrollbars: "no" });
        }
        function validateSlideImageRequired() {
            var href = $("#image-selector input[name='Href']").val();
            if (href == '') {
                alert('Your must add an image to your slide before saving it.');
                return false;
            }
            return true;
        }
        function clearImage() {
            $("#image-selector").find("input[type='text']").val('');
        }
        function selectFile(evt, urlInput) {
            epiFileManagerDialog(urlInput.val(), function (a, b) {
                if (a.closeAction == 'insert') {
                    var selectedPath = a.items[0].path;
                    urlInput.val(selectedPath);
                } else if (a.closeAction == 'clear') {
                    clearImage();
                }
            });
        }
        function ImageSelectorControls() {
            var imageSelectorContainer = $("<div>");
            var urlInputControl = $("<input id='image-url' type='text' name='Href' readonly='true' disabled='disabled'></input>").val(imageSlide.ImageUrl);
            imageSelectorContainer
                .append(urlInputControl)
                .append($("<input class='epismallbutton' type='button' value='...'>").click(function (evt) { selectFile(evt, urlInputControl); }));
            return imageSelectorContainer;
        }
        function ThumbnailSelectorControls() {
            var thumbnailSelectorContainer = $("<div>");
            var urlInputControl = $("<input id='thumbnail-url' type='text' name='ThumbnailHref' readonly='true' disabled='disabled'></input>").val(imageSlide.ThumbnailUrl);
            thumbnailSelectorContainer
                .append(urlInputControl)
                .append($("<input class='epismallbutton' type='button' value='...'>").click(function (evt) { selectFile(evt, urlInputControl); }));
            return thumbnailSelectorContainer;
        }
        function SaveButtonClick(e) {
            e.preventDefault();
            if (!validateSlideImageRequired()) {
                return;
            }
            EPi.GetDialog().Close({
                ImageUrl: value($("#image-url")),
                ImageLinkUrl: value($("#image-link")),
                ImageTooltip: value($("#image-caption")),
                ThumbnailUrl: value($("#thumbnail-url"))
            });
        }
        function CancelButtonClick() {
            EPi.GetDialog().Close();
        }
        function value(control, value) {
            if (arguments.length == 1 && control != null) {
                return control.val();
            }
            control.val(value);
        }
        function initialize() {
            $("#save-button").click(SaveButtonClick);
            $("#cancel-button").click(CancelButtonClick);
        
            imageSlide = EPi.GetDialog().dialogArguments.state;

            $("#image-selector").append(ImageSelectorControls());
            $("#thumbnail-selector").append(ThumbnailSelectorControls());

            value($("#image-url"), imageSlide.ImageUrl);
            value($("#image-link"), imageSlide.ImageLinkUrl);
            value($("#image-caption"), imageSlide.ImageTooltip);
            value($("#thumbnail-url"), imageSlide.ThumbnailUrl);
        }
        $(document).ready(initialize);
    // ]]>
    </script>
    
    <style type="text/css">
        body {
            background-color: #F4F4F4;
            color: #000000;
            font: 75%/1.231 "Lucida Grande","Lucida Sans Unicode",Arial,Verdana,Sans-Serif;
            text-align: left;
        }
        h1 {
            font-size: 18px;
            margin-bottom: 7px;
            margin-top: 0;
        }
        #image-selector,
        #thumbnail-selector {
            margin-bottom: 10px;
        }
        #image-url,
        #thumbnail-url {
            width: 350px;
        }
        #image-text {
            margin-bottom: 10px;
        }
        #image-caption,
        #image-link {
            width: 382px;
        }
        
        .image-link-url,
        #image-text {
            margin-bottom: 10px;
        }
    </style>

    <h1><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.SlideEditor.Title %>" /></h1>
    <div class="panel_wrapper" id="editors">
        <div id="image-selector">
            <label for="Href"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.SlideEditor.Image %>" /></label>
        </div>
        
        <div class="image-link-url">
            <label for="image-link"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.SlideEditor.ImageLinksUrl %>" /></label>
            <div><input type="text" name="image-link" id="image-link" /></div>
        </div>
    
        <div id="image-text">
            <label for="image-caption"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.SlideEditor.ImageCaption %>" /></label>
            <div><textarea name="image-caption" id="image-caption" rows="7" cols="30"></textarea></div>
        </div>
        
        <div id="thumbnail-selector">
            <label for="ThumbnailHref"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.SlideEditor.Thumbnail %>" /></label>
        </div>
    </div>
    <div style="clear: both; position: relative;">
        <div style="position: absolute; right: 0;">
            <span class="epi-cmsButton">
                <input id="save-button" type="button" class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Save" onmouseover="EPi.ToolButton.MouseDownHandler(this)" onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)" value="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/SlideEditor/SaveButtonText") %>" />
            </span>
            <span class="epi-cmsButton">
                <input id="cancel-button" type="button" class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Cancel" onmouseover="EPi.ToolButton.MouseDownHandler(this)" onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)" value="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/SlideEditor/CancelButtonText") %>" />
            </span>
        </div>
    </div>
</asp:Content>
