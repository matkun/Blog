var ImageMap = ImageMap || {};

ImageMap.EPiServerButtons = ImageMap.EPiServerButtons || function () {
    function createEPiServerButton(text, cssClass, callbackFunction) {
        return $("<span class='epi-cmsButton'></span>")
                .append($("<input type='button'></input>")
                    .val(text)
                    .addClass(["epi-cmsButton-tools", cssClass].join(' '))
                    .mouseover(function () { EPi.ToolButton.MouseDownHandler(this); })
                    .mouseout(function () { EPi.ToolButton.ResetMouseDownHandler(this); })
                    .click(callbackFunction)
                );
    };

    return {
        MoveUp: function (callbackFunction) {
            return createEPiServerButton('', 'epi-cmsButton-Up', callbackFunction);
        },
        MoveDown: function (callbackFunction) {
            return createEPiServerButton('', 'epi-cmsButton-Down', callbackFunction);
        },
        EditForm: function (callbackFunction) {
            return createEPiServerButton('', 'epi-cmsButton-Edit', callbackFunction);
        },
        RemoveForm: function (callbackFunction) {
            return createEPiServerButton('', 'epi-cmsButton-Delete', callbackFunction);
        }
    };
} ();

ImageMap.EPiServerPopup = ImageMap.EPiServerPopup || function (baseUrl, initialWidth, initialHeight) {
    var width = initialWidth;
    var height = initialHeight;

    function successCallback(returnValue, onCompleteArgs) {
        if (returnValue != null) {
            EPi.PageLeaveCheck.SetPageChanged(true);

            var item = new Object();
            item.HotSpotId = returnValue.HotSpotId;
            item.Tooltip = returnValue.Tooltip;
            item.ClickTargetValue = returnValue.ClickTargetValue;
            item.ClickTargetDisplay = returnValue.ClickTargetDisplay;
            item.XPosition = returnValue.XPosition;
            item.YPosition = returnValue.YPosition;
            item.Width = returnValue.Width;
            item.Height = returnValue.Height;
            onCompleteArgs.callBack(item);
        }
    };

    return {
        getWidth: function () { return width; },
        getHeight: function () { return height; },
        setSize: function (newWidth, newHeight) {
            width = newWidth;
            height = newHeight;
        },
        show: function (state, imageUrl, reservedPositions, updateItem) {
            var onCompleteArgs = { callBack: updateItem };

            var args = new Object();
            args.imageUrl = imageUrl;
            args.reservedPositions = reservedPositions;
            args.state = state;
            args.parentWindow = document.window;
            args.hideBookmarks = true;

            var options = { 'width': width, 'height': height, scrollbars: "no" };
            EPi.CreateDialog(baseUrl, successCallback, onCompleteArgs, args, options);
        }
    };
};

ImageMap.PropertyCollection = function (containerId, uniqueIdentifier, collectionChangedCallback) {
    function getOrCreateHiddenField() {
        var hfName = ["hidden-updated-map-", uniqueIdentifier].join('');
        var hiddenField = $(containerId).find("input[type='hidden'][name='" + hfName + "']");
        if (hiddenField.length > 0) {
            return hiddenField;
        }
        hiddenField = $("<input type='hidden'></input>").attr({ name: hfName });
        $(containerId).append(hiddenField);
        return hiddenField;
    };
    
    function imageMap(imageMapObject) {
        if (arguments.length == 0) {
            return eval('(' + getOrCreateHiddenField().val() + ')');
        } else if (typeof (imageMapObject) == typeof ("")) {
            getOrCreateHiddenField().val(imageMapObject);
            collectionChangedCallback();
        } else {
            getOrCreateHiddenField().val(JSON.stringify(imageMapObject));
            collectionChangedCallback();
        }
    };

    return {
        setJson: function (json) {
            imageMap(json);
        },
        getImageMap: function () {
            return imageMap();
        },
        hotSpotAt: function (i) {
            return imageMap().HotSpots[i];
        },
        addHotSpot: function (hotSpot) {
            var imageMapObject = imageMap();
            imageMapObject.HotSpots[imageMapObject.HotSpots.length] = hotSpot;
            imageMap(imageMapObject);
        },
        replaceHotSpot: function (i, hotSpot) {
            var copy = imageMap();
            copy.HotSpots[i] = hotSpot;
            imageMap(copy);
        },
        deleteHotSpotAt: function (i) {
            var imageMapObject = imageMap();
            var newCollection = $.grep(imageMapObject.HotSpots, function (element, j) {
                return (i != j);
            });
            imageMapObject.HotSpots = newCollection;
            imageMap(imageMapObject);
        },
        moveItemUp: function (i) {
            var imageMapObject = imageMap();
            var hotSpotArray = imageMapObject.HotSpots;
            var copy = hotSpotArray[i - 1];
            hotSpotArray[i - 1] = hotSpotArray[i];
            hotSpotArray[i] = copy;
            imageMapObject.HotSpots = hotSpotArray;
            imageMap(imageMapObject);
        },
        moveItemDown: function (i) {
            var imageMapObject = imageMap();
            var hotSpotArray = imageMapObject.HotSpots;
            var copy = hotSpotArray[i + 1];
            hotSpotArray[i + 1] = hotSpotArray[i];
            hotSpotArray[i] = copy;
            imageMapObject.HotSpots = hotSpotArray;
            imageMap(imageMapObject);
        },
        updateImageUrl: function (updatedImageMap) {
            updatedImageMap.HotSpots = imageMap().HotSpots;
            imageMap(updatedImageMap);
        }
    };
};

ImageMap.EditModeImageMap = ImageMap.EditModeImageMap || function (uniqueIdentifier, hotSpotEditorUrl, initialImageMap, emptyShow) {
    var imageMapContainerId = ["#image-map-", uniqueIdentifier].join('');
    var imageMapUrlControl;
    var imageMapPropertyCollection;
    var hotSpotEditor;

    function removeItem(i) {
        imageMapPropertyCollection.deleteHotSpotAt(i);
    };

    function moveUp(i) {
        imageMapPropertyCollection.moveItemUp(i);
    };
    function createMoveUpButton(items, i) {
        var td = $("<td>").css({ 'padding-left': '12px' });
        var epiMoveUpButton = ImageMap.EPiServerButtons.MoveUp(function () { moveUp(i); });
        if (i == 0) {
            epiMoveUpButton.css('visibility', 'hidden');
        }
        td.append(epiMoveUpButton);
        return td;
    };
    function moveDown(i) {
        imageMapPropertyCollection.moveItemDown(i);
    };
    function createMoveDownButton(items, i) {
        var td = $("<td>").css({ 'padding-left': '6px' });
        var epiMoveDownButton = ImageMap.EPiServerButtons.MoveDown(function () { moveDown(i); });
        if (i == items.length - 1) {
            epiMoveDownButton.css('visibility', 'hidden');
        }
        td.append(epiMoveDownButton);
        return td;
    };

    function editItem(i) {
        var currentHotSpot = imageMapPropertyCollection.hotSpotAt(i);
        var reservedPositions = $.grep(imageMapPropertyCollection.getImageMap().HotSpots, function (element, j) {
            return (i != j);
        });

        hotSpotEditor.show(currentHotSpot, imageMapUrlControl.val(), JSON.stringify(reservedPositions), function (updatedItem) {
            imageMapPropertyCollection.replaceHotSpot(i, updatedItem);
        });
    };
    function createEditButton(i) {
        return $("<td>").css({ 'padding-left': '12px' }).append(ImageMap.EPiServerButtons.EditForm(function () { editItem(i); }));
    };
    function createRemoveButton(i) {
        return $("<td>").css({ 'padding-left': '12px' }).append(ImageMap.EPiServerButtons.RemoveForm(function () { removeItem(i); }));
    };

    function createHotSpotTargetCell(item) {
        return $("<td>")
            .text(unescape(item.ClickTargetDisplay))
            .attr('title', "This hot spot is a link to the page " + unescape(item.ClickTargetDisplay) + ". The text that is shown is '" + unescape(item.Tooltip) + "'.")
            .css({ 'max-height': '100px', 'overflow': 'hidden' });
    };

    function clearHotSpots() {
        $(imageMapContainerId).find(".hot-spot-table tr")
            .filter(function (i) { return (i > 0); })
            .remove();
    };

    function addNewPopup() {
        var reservedPositions = imageMapPropertyCollection.getImageMap().HotSpots;
        hotSpotEditor.show(emptyShow, imageMapUrlControl.val(), JSON.stringify(reservedPositions), function (updatedItem) { imageMapPropertyCollection.addHotSpot(updatedItem); });
    };

    function addHotSpots() {
        var repeater = $(imageMapContainerId).find(".hot-spot-table");
        var hotSpots = imageMapPropertyCollection.getImageMap().HotSpots;

        $.each(hotSpots, function (i, hotSpot) {
            var tr = $("<tr>")
                .attr('hot-spot-id', hotSpot.HotSpotId)
                .addClass('hot-spot-row')
                .append(createMoveUpButton(hotSpots, i))
                .append(createMoveDownButton(hotSpots, i))
                .append(createHotSpotTargetCell(hotSpot))
                .append(createEditButton(i))
                .append(createRemoveButton(i));
            repeater.append(tr);
        });
    };

    function updateImageMapInterface() {
        clearHotSpots();
        addHotSpots();
        refreshPreviewImageFromControl();
    };

    function refreshPreviewHotSpots() {
        $("div.preview-hot-spot").remove();

        var hotSpots = imageMapPropertyCollection.getImageMap().HotSpots;
        var container = $(imageMapContainerId).find(".image-map-preview-container");

        var totalImageWidth = container.find('img').outerWidth();
        var totalImageHeight = container.find('img').outerHeight();

        $.each(hotSpots, function (i, hotSpot) {
            var previewHotSpot = $("<div>")
                                    .attr('hot-spot-id', hotSpot.HotSpotId)
                                    .attr('title', unescape(hotSpot.Tooltip))
                                    .addClass('preview-hot-spot')
                                    .css({
                                        'left': hotSpot.XPosition * totalImageWidth,
                                        'top': hotSpot.YPosition * totalImageHeight,
                                        'width': hotSpot.Width * totalImageWidth,
                                        'height': hotSpot.Height * totalImageHeight
                                    });
            previewHotSpot.append($("<div>").addClass('dutt')); // For holding the round centered dot background image.

            container.append(previewHotSpot);
        });
    }

    function showHotSpotConnection() {
        var previewHotSpots = $(imageMapContainerId).find(".image-map-preview-container .preview-hot-spot");
        var hotSpotRows = $(imageMapContainerId).find(".hot-spot-table tr.hot-spot-row");
        var currentHotSpotId = $(this).attr('hot-spot-id');

        $.each(previewHotSpots, function (i, previewHotSpot) {
            if ($(this).attr('hot-spot-id') == currentHotSpotId) {
                $(this).addClass('highlighted-hot-spot');
                return false;
            }
        });
        $.each(hotSpotRows, function (i, hotSpotRow) {
            if ($(this).attr('hot-spot-id') == currentHotSpotId) {
                $(this).addClass('highlighted-hot-spot');
                return false;
            }
        });
    }
    function hideHotSpotConnection() {
        $(imageMapContainerId).find(".highlighted-hot-spot").removeClass('highlighted-hot-spot');
    }

    function refreshImageUrlJson() {
        var imageMap = imageMapPropertyCollection.getImageMap();
        var imageUrlControl = imageMapUrlControl;
        imageMap.ImageUrl = imageUrlControl.val();
        imageMapPropertyCollection.updateImageUrl(imageMap);
    };
    function updatePopupWindowSize() {
        var img = $(imageMapContainerId).find("div.image-map-preview-container img");
        var newHeight = img.height() + 240;
        var newWidth = hotSpotEditor.getWidth(); // Old width is still fine.
        hotSpotEditor.setSize(newWidth, newHeight);
    };
    function refreshPreviewImageFromControl() {
        var imageMap = imageMapPropertyCollection.getImageMap();
        var imageMapPreviewContainer = $(imageMapContainerId).find("div.image-map-preview-container");
        imageMapPreviewContainer.empty();

        if (imageMapUrlControl == null) {
            return;
        }
        var hotSpotControls = $(imageMapContainerId).find("div.hot-spot-controls");
        if (imageMapUrlControl.val() == null || imageMapUrlControl.val() == '') {
            imageMap.ImageUrl = "";
            hotSpotControls.hide();
            return;
        }
        imageMap.ImageUrl = imageMapUrlControl.val();

        var image = $("<img name='image-map' alt='Preview of interactive image map with hot spots.' style='max-width: 607px;' />");
        imageMapPreviewContainer.append(image);

        image.bind('load', function() {
            updatePopupWindowSize();
            refreshPreviewHotSpots();
        })
        .attr('src', imageMapUrlControl.val())
        .each(function() {
            //Cache fix for browsers that don't trigger .load()
            if (this.complete) $(this).trigger('load');
        });

        hotSpotControls.show();
    };
    function epiFileManagerDialog(selectedFile, onClose) {
        var onCompleteArguments = new Object();
        var dialogArguments = new Object();
        dialogArguments.hideBookmarks = true;
        dialogArguments.parentWindow = document.window;
        var fileManagerBrowserUrl = EPi.ResolveUrlFromUI("edit/FileManagerBrowser.aspx") + "?selectedfile=" + selectedFile;
        EPi.CreateDialog(fileManagerBrowserUrl, onClose, onCompleteArguments, dialogArguments, { width: 660, height: 460, scrollbars: "no" });
    };
    function clearImageMapUrl() {
        imageMapUrlControl.val('');
    };
    function selectImageMapImage(evt, urlInput) {
        epiFileManagerDialog(urlInput.val(), function (a, b) {
            if (a.closeAction == 'insert') {
                var selectedPath = a.items[0].path;
                urlInput.val(selectedPath);
                EPi.PageLeaveCheck.SetPageChanged(true);
            } else if (a.closeAction == 'clear') {
                clearImageMapUrl();
                EPi.PageLeaveCheck.SetPageChanged(true);
            }
            refreshPreviewImageFromControl();
            refreshImageUrlJson();
        });
    };
    function imageSelectorControls() {
        var imageSelectorContainer = $("<div>");
        var urlControlId = 'image-url-' + uniqueIdentifier;
        var urlInputLabel = $('<label>Välj bild: </label>').attr('for', urlControlId).attr('title', 'First select an image from EPiServers file archive and then add linked hot spot areas.');
        var urlInputControl = $("<input type='text' style='width: 300px;' readonly='readonly' disabled='disabled'></input>").attr('id', urlControlId);
        imageSelectorContainer
            .append(urlInputLabel)
            .append(urlInputControl)
            .append($("<input class='epismallbutton' type='button' value='...'>").click(function (evt) { selectImageMapImage(evt, urlInputControl); }));
        return imageSelectorContainer;
    };
    function initializeInterfaceFromJson() {
        imageMapUrlControl.val(imageMapPropertyCollection.getImageMap().ImageUrl);
        updateImageMapInterface();
    }

    return {
        newItem: function () {
            addNewPopup();
        },

        init: function () {
            var imageMapUrlContainer = $(imageMapContainerId).find("div.image-map-url-container");
            imageMapUrlContainer.append(imageSelectorControls());
            imageMapUrlControl = imageMapUrlContainer.find("input[type='text']");

            hotSpotEditor = ImageMap.EPiServerPopup(hotSpotEditorUrl, 645, 540);
            imageMapPropertyCollection = ImageMap.PropertyCollection(imageMapContainerId, uniqueIdentifier, updateImageMapInterface);
            imageMapPropertyCollection.setJson(initialImageMap);
            initializeInterfaceFromJson();

            var previewHotSpots = $(imageMapContainerId).find(".image-map-preview-container .preview-hot-spot");
            var hotSpotRows = $(imageMapContainerId).find(".hot-spot-table tr.hot-spot-row");
            previewHotSpots.live('mouseover', showHotSpotConnection);
            hotSpotRows.live('mouseover', showHotSpotConnection);
            previewHotSpots.live('mouseout', hideHotSpotConnection);
            hotSpotRows.live('mouseout', hideHotSpotConnection);
        }
    };
};
