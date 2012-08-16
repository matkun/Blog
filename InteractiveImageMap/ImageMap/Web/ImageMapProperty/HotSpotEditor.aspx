<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="HotSpotEditor.aspx.cs" Inherits="EPiServer.ImageMap.Web.ImageMapProperty.HotSpotEditor" MasterPageFile="~/ImageMap/Web/MasterPages/PluginMaster.Master" %>

<asp:Content ContentPlaceHolderID="FullRegion" runat="server">
    <style type="text/css">
        #image-map-container {
            display: inline-block;
            position: relative;
        }
        #click-target-container,
        #tooltip-container  {
            margin-top: 13px;
        }
        #click-target-container input {
            margin-top: 3px;
        }
        #tooltip {
            margin-top: 3px;
            width: 300px;
        }
        .reserved-hot-spot-position {
            border: 1px solid #ffffff;
            background: url("/imagemap/web/style/img/background-grey.png") repeat scroll 0 0 transparent;
            z-index: 100;
            position: absolute;
        }
    </style>

    <h1>Add or edit an image hot spot</h1>
    <div class="panel_wrapper" id="editors">
        <div>Select an area on the image to add a clickable hot spot (minimum area size is 30x30 pixels)</div><br/>
        <div id="image-map-container"></div><br/>
        
        <div id="click-target-container">
            <label for="click-target_Display">Select a hot spot target; for instance an internal EPiServer page or an external link page</label>
            <input type="hidden" id="click-target_Value" name="click-target_Value" />
            <input type="text" id="click-target_Display" name="click-target_Display" style="width:300px;" disabled="disabled" />

            <%-- TODO - Change path below to EPi PageBrowserDialog to point to your UI --%>
            <input id="click-target-button" name="click-target-button" class="epismallbutton" type="button" onclick="EPi.CreatePageBrowserDialog('/secure/ui/CMS/edit/pagebrowser.aspx',document.getElementById('click-target_Value').value,'False','False','click-target_Display','click-target_Value','sv', null, null, false);" value="...">
        </div>
        
        <div id="tooltip-container">
            <label for="tooltip">Write a text that will be shown when the visitor hovers over the hot spot</label>
            <input id="tooltip" name="tooltip" type="text" />
        </div>
        
        <input type="hidden" name="hot-spot-id" id="hot-spot-id" />
        <input type="hidden" name="x-position" id="x-position" />
        <input type="hidden" name="y-position" id="y-position" />
        <input type="hidden" name="hot-spot-width" id="hot-spot-width" />
        <input type="hidden" name="hot-spot-height" id="hot-spot-height" />
    </div>
    <div style="clear: both;">
        <div style="position: absolute; right: 0;">
            <span class="epi-cmsButton">
                <input id="save-button" type="button" class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Save" onmouseover="EPi.ToolButton.MouseDownHandler(this)" onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)" value="Save" />
            </span>
            <span class="epi-cmsButton">
                <input id="cancel-button" type="button" class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Cancel" onmouseover="EPi.ToolButton.MouseDownHandler(this)" onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)" value="Cancel" />
            </span>
        </div>
    </div>
    
    <script type="text/javascript">
    // <![CDATA[
        var hotSpot;
        var imageUrl;
        var reservedPositions;

        function generateGuid() {
            var randomizeFour = function () {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            };
            return (randomizeFour() + randomizeFour() + "-" + randomizeFour() + "-" + randomizeFour() + "-" + randomizeFour() + "-" + randomizeFour() + randomizeFour() + randomizeFour());
        }
        function isNullOrEmpty(value) {
            return value == null || value == '';
        }
        function validateClickTargetRequired() {
            if (isNullOrEmpty($("#click-target_Display").val()) || isNullOrEmpty($("#click-target_Value").val())) {
                alert('You must select a hot spot target before you can save; for instance an internal EPiServer page or a shortcut page and so on.');
                return false;
            }
            return true;
        }
        function validateHotSpotPositionRequired() {
            var xPosition = $("#x-position").val();
            var yPosition = $("#y-position").val();
            if (isNullOrEmpty(xPosition) || isNullOrEmpty(yPosition)) {
                alert('You must select a clickable hot spot area in the image before you can save; click and drag the mouse cursor over the image.');
                return false;
            }
            return true;
        }
        function SaveButtonClick(e) {
            e.preventDefault();
            if (!validateClickTargetRequired()) {
                return;
            }
            if (!validateHotSpotPositionRequired()) {
                return;
            }
            EPi.GetDialog().Close({
                HotSpotId: value($("#hot-spot-id")),
                Tooltip: escape(value($("#tooltip"))),
                ClickTargetValue: value($("#click-target_Value")),
                ClickTargetDisplay: escape(value($("#click-target_Display"))),
                XPosition: value($("#x-position")),
                YPosition: value($("#y-position")),
                Width: value($("#hot-spot-width")),
                Height: value($("#hot-spot-height"))
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
        function ensureHotSpotId() {
            if (isNullOrEmpty($("#hot-spot-id").val())) {
                $("#hot-spot-id").val(generateGuid());
            }
        }
        function ImageMapControls() {

            var image = $("<img id='image-map' name='image-map' style='max-width: 607px;' />")
            .bind('load', function () {
                drawReservedHotSpotPositions();
                drawInitialHotSpot();
            })
            .attr('src', imageUrl)
            .each(function () {
                //Cache fix for browsers that don't trigger .load()
                if (this.complete) $(this).trigger('load');
            });
            return image;
        }
        function initializePageLeaveChecks() {
            EPi.PageLeaveCheck.confirmMessage = "You have made changes to this hot spot; these will be lost if you continue without saving.";
            $("#save-button").click(function () { EPi.PageLeaveCheck.SetPageChanged(false); });
        }
        function drawInitialHotSpot() {
            var xPosition = $("#x-position").val();
            var yPosition = $("#y-position").val();
            var height = $("#hot-spot-height").val();
            var width = $("#hot-spot-width").val();
            if(isNullOrEmpty(xPosition) || isNullOrEmpty(yPosition) || isNullOrEmpty(height) || isNullOrEmpty(width)) {
                return;
            }
            
            var totalImageWidth = $('#image-map-container img').outerWidth();
            var totalImageHeight = $('#image-map-container img').outerHeight();

            var initialBox = $(document.createElement('div'))
                                .attr('title', $("#tooltip").val())
                                .addClass("ui-boxer-box").css({
                                    'left': xPosition * totalImageWidth,
                                    'top' : yPosition * totalImageHeight,
                                    'width' : width * totalImageWidth,
                                    'height' : height * totalImageHeight,
                                    'border': '1px solid white',
                                    'background': 'url("/imagemap/web/style/img/background-pink.png") repeat scroll 0 0 transparent',
                                    'z-index': 100,
                                    'position': 'absolute'
                                });
            $('#image-map-container').append(initialBox);
        }

        function drawReservedHotSpotPositions() {
            $.each(reservedPositions, function (i, hotSpot) {
                var totalImageWidth = $('#image-map-container img').outerWidth();
                var totalImageHeight = $('#image-map-container img').outerHeight();

                var reservedBox = $(document.createElement('div'))
                                .attr('title', unescape(hotSpot.Tooltip))
                                .addClass("reserved-hot-spot-position")
                                .css({
                                    'left': hotSpot.XPosition * totalImageWidth,
                                    'top': hotSpot.YPosition * totalImageHeight,
                                    'width': hotSpot.Width * totalImageWidth,
                                    'height': hotSpot.Height * totalImageHeight
                                });
                $('#image-map-container').append(reservedBox);
            });
        }
        function initialize() {
            initializePageLeaveChecks();

            $("#cancel-button").click(CancelButtonClick);
            $("#save-button").click(SaveButtonClick);
            
            var dialogArguments = EPi.GetDialog().dialogArguments;
            hotSpot = dialogArguments.state;
            imageUrl = dialogArguments.imageUrl;
            reservedPositions = eval('(' + dialogArguments.reservedPositions + ')');

            $('#image-map-container').append(ImageMapControls());

            value($("#hot-spot-id"), hotSpot.HotSpotId);
            value($("#click-target_Value"), hotSpot.ClickTargetValue);
            value($("#x-position"), hotSpot.XPosition);
            value($("#y-position"), hotSpot.YPosition);
            value($("#hot-spot-width"), hotSpot.Width);
            value($("#hot-spot-height"), hotSpot.Height);

            var tooltip = hotSpot.Tooltip;
            if (tooltip != null) {
                tooltip = unescape(tooltip);
            }
            value($("#tooltip"), tooltip);

            var display = hotSpot.ClickTargetDisplay;
            if(display != null) {
                display = unescape(display);
            }
            value($("#click-target_Display"), display);

            ensureHotSpotId();
        }
        $(document).ready(initialize);
    // ]]>
    </script>

    <script language="javascript" type="text/javascript">
    // <![CDATA[
        // Enhanced Boxer plugin, also modified for jQuery 1.8. Original is found at
        // Demo: http://jsbin.com/azare
        // Code: http://jsbin.com/azare/edit

        $.widget("ui.boxer", $.ui.mouse, {
            options: {
                    appendTo: 'body',
                    distance: 0
                },

            _init: function () {
                this.element.addClass("ui-boxer");
                this.dragged = false;
                this._mouseInit();
                this.helper = $('<div>')
			        .css({ border: '1px dotted #ffffff', 'background-image': 'url("/imagemap/web/style/img/background-pink.png") repeat scroll 0 0 transparent' })
			        .addClass("ui-boxer-helper");
            },

            destroy: function () {
                this.element
			        .removeClass("ui-boxer ui-boxer-disabled")
			        .removeData("boxer")
			        .unbind(".boxer");
                this._mouseDestroy();

                return this;
            },

            _mouseStart: function (event) {
                var offset = $("#image-map-container").offset();
                var xCoordinate = event.pageX - offset.left;
                var yCoordinate = event.pageY - offset.top;

                this.opos = [xCoordinate, yCoordinate];
                if (this.options.disabled) {
                    return;
                }

                this._trigger("start", event);
                $("#image-map-container").append(this.helper);
                this.helper.css({
                    "z-index": 100,
                    "position": "absolute",
                    "background": 'url("/imagemap/web/style/img/background-pink.png") repeat scroll 0 0 transparent',
                    "left": xCoordinate,
                    "top": yCoordinate,
                    "width": 0,
                    "height": 0
                });
            },

            _mouseDrag: function (event) {
                this.dragged = true;

                var offset = $("#image-map-container").offset();
                var xCoordinate = event.pageX - offset.left;
                var yCoordinate = event.pageY - offset.top;

                if (this.options.disabled) {
                    return;
                }

                var x1 = this.opos[0], y1 = this.opos[1], x2 = xCoordinate, y2 = yCoordinate;
                if (x1 > x2) { var tmp = x2; x2 = x1; x1 = tmp; }
                if (y1 > y2) { var tmp = y2; y2 = y1; y1 = tmp; }
                this.helper.css({ left: x1, top: y1, width: x2 - x1, height: y2 - y1 });

                this._trigger("drag", event);

                return false;
            },

            _mouseStop: function (event) {
                EPi.PageLeaveCheck.SetPageChanged(true);

                this.dragged = false;
                $('.ui-boxer-box').remove();
                var position = this.helper.position();
                var totalImageWidth = $('#image-map-container img').outerWidth();
                var totalImageHeight = $('#image-map-container img').outerHeight();
                var boxWidth = parseInt(this.helper.css('width'), 10);
                var boxHeight = parseInt(this.helper.css('height'), 10);
                var minWidth = 30;
                var minHeight = 30;
                
                <%-- Make sure that the selected area is not outside (above or left of) the image. --%>
                if (position.left < 0) {
                    boxWidth = boxWidth - Math.abs(position.left);
                    this.helper.css({ 'left': '0', 'width': boxWidth });
                }
                if (position.top < 0) {
                    boxHeight = boxHeight - Math.abs(position.top);
                    this.helper.css({ 'top': '0', 'height': boxHeight });
                }
                
                <%-- Make sure that the selected area offset allows for at least 30x30px area size (minWidth x MinHeight). --%>
                var maxOffsetX = totalImageWidth - minWidth;
                if(position.left > maxOffsetX) {
                    this.helper.css('left', maxOffsetX);
                }
                var maxOffsetY = totalImageHeight - minHeight;
                if(position.top > maxOffsetY) {
                    this.helper.css('top', maxOffsetY);
                }
                
                <%-- Make sure that the selected area is not outside (below or right of) the image. --%>
                if (position.left + boxWidth > totalImageWidth) {
                    boxWidth = totalImageWidth - position.left;
                    this.helper.css('width', boxWidth);
                }
                if (position.top + boxHeight > totalImageHeight) {
                    boxHeight = totalImageHeight - position.top;
                    this.helper.css('height', boxHeight);
                }
                
                <%-- Make sure the selected area is at least 30x30px (minWidth x MinHeight). --%>
                if(boxWidth < minWidth) {
                    this.helper.css('width', minWidth);
                }
                if(boxHeight < minHeight) {
                    this.helper.css('height', minHeight);
                }
                
                var clone = this.helper.clone()
			        .removeClass('ui-boxer-helper')
                    .addClass('ui-boxer-box')
                    .appendTo(this.element);

                this._trigger("stop", event, { box: clone });
                this.helper.remove();
                return false;
            }
        });
    // ]]>
    </script>
    <script language="javascript" type="text/javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('#image-map-container').boxer({
                stop: function (event, ui) {
                    var position = ui.box.position();
                    ui.box.css({ border: '1px solid white', background: 'url("/imagemap/web/style/img/background-pink.png") repeat scroll 0 0 transparent' });
                    
                    var totalImageWidth = $('#image-map-container img').outerWidth();
                    var totalImageHeight = $('#image-map-container img').outerHeight();
                    
                    $("#x-position").val(position.left / totalImageWidth);
                    $("#y-position").val(position.top / totalImageHeight);
                    $("#hot-spot-width").val(ui.box.width() / totalImageWidth);
                    $("#hot-spot-height").val(ui.box.height() / totalImageHeight);
                }
            });
        });
    // ]]>
    </script>
</asp:Content>
