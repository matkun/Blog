var ImageSlideShow = ImageSlideShow || {};

ImageSlideShow.EPiServerButtons = ImageSlideShow.EPiServerButtons || function () {
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

ImageSlideShow.EPiServerPopup = ImageSlideShow.EPiServerPopup || function (baseUrl, width, height) {
    function successCallback(returnValue, onCompleteArgs) {
        if (returnValue != null) {
            var item = new Object();
            item.ImageUrl = returnValue.ImageUrl;
            item.ImageLinkUrl = returnValue.ImageLinkUrl;
            item.ImageTooltip = returnValue.ImageTooltip;
            item.ThumbnailUrl = returnValue.ThumbnailUrl;
            onCompleteArgs.callBack(item);
        }
    };

    return {
        show: function (state, updateItem) {
            var onCompleteArgs = { callBack: updateItem };

            var args = new Object();
            args.state = state;
            args.parentWindow = document.window;
            args.hideBookmarks = true;

            var options = { 'width': width, 'height': height, scrollbars: "no" };
            EPi.CreateDialog(baseUrl, successCallback, onCompleteArgs, args, options);
        }
    };
};

ImageSlideShow.PropertyCollection = function (containerId, uniqueIdentifier, collectionChangedCallback) {
    function getOrCreateHiddenField() {
        var hfName = ["hidden-updated-show-", uniqueIdentifier].join('');
        var hiddenField = $(containerId).find("input[type='hidden'][name='" + hfName + "']");
        if (hiddenField.length > 0) {
            return hiddenField;
        }
        hiddenField = $("<input type='hidden'></input>").attr({ name: hfName });
        $(containerId).append(hiddenField);
        return hiddenField;
    };

    function slideShow(slideShowObject) {
        if (arguments.length == 0) {
            return eval('(' + getOrCreateHiddenField().val() + ')');
        } else if (typeof (slideShowObject) == typeof ("")) {
            getOrCreateHiddenField().val(slideShowObject);
            collectionChangedCallback();
        } else {
            getOrCreateHiddenField().val($.toJSON(slideShowObject));
            collectionChangedCallback();
        }
    };

    return {
        setJson: function (json) {
            slideShow(json);
        },
        getSlideShow: function () {
            return slideShow();
        },
        imageSlideAt: function (i) {
            return slideShow().Slides[i];
        },
        addImageSlide: function (slide) {
            var slideShowObject = slideShow();
            slideShowObject.Slides[slideShowObject.Slides.length] = slide;
            slideShow(slideShowObject);
        },
        replaceImageSlide: function (i, slide) {
            var copy = slideShow();
            copy.Slides[i] = slide;
            slideShow(copy);
        },
        deleteImageSlideAt: function (i) {
            var slideShowObject = slideShow();
            var newCollection = $.grep(slideShowObject.Slides, function (element, j) {
                return (i != j);
            });
            slideShowObject.Slides = newCollection;
            slideShow(slideShowObject);
        },
        moveItemUp: function (i) {
            var slideShowObject = slideShow();
            var slideArray = slideShowObject.Slides;
            var copy = slideArray[i - 1];
            slideArray[i - 1] = slideArray[i];
            slideArray[i] = copy;
            slideShowObject.Slides = slideArray;
            slideShow(slideShowObject);
        },
        moveItemDown: function (i) {
            var slideShowObject = slideShow();
            var slideArray = slideShowObject.Slides;
            var copy = slideArray[i + 1];
            slideArray[i + 1] = slideArray[i];
            slideArray[i] = copy;
            slideShowObject.Slides = slideArray;
            slideShow(slideShowObject);
        },
        updateGlobalSettings: function (updatedSlideShow) {
            updatedSlideShow.Slides = slideShow().Slides;
            slideShow(updatedSlideShow);
        }
    };
};

ImageSlideShow.EditModeImageSlideCollection = ImageSlideShow.EditModeImageSlideCollection || function (uniqueIdentifier, imageSlideEditorUrl, initialShow, emptyShow) {
    var slideContainerId = ["#image-slide-collection-", uniqueIdentifier].join('');
    var globalSettingsContainerId = ["#global-slide-show-settings-", uniqueIdentifier].join('');
    var imageSlideShowPropertyCollection;
    var imageSlideEditor;

    function removeItem(i) {
        imageSlideShowPropertyCollection.deleteImageSlideAt(i);
    };

    function moveUp(i) {
        imageSlideShowPropertyCollection.moveItemUp(i);
    };

    function createMoveUpButton(items, i) {
        var td = $("<td>");
        var epiMoveUpButton = ImageSlideShow.EPiServerButtons.MoveUp(function () { moveUp(i); });
        if (i == 0) {
            epiMoveUpButton.css('visibility', 'hidden');
        }
        td.append(epiMoveUpButton);
        return td;
    };

    function moveDown(i) {
        imageSlideShowPropertyCollection.moveItemDown(i);
    };

    function createMoveDownButton(items, i) {
        var td = $("<td>");
        var epiMoveDownButton = ImageSlideShow.EPiServerButtons.MoveDown(function () { moveDown(i); });
        if (i == items.length - 1) {
            epiMoveDownButton.css('visibility', 'hidden');
        }
        td.append(epiMoveDownButton);
        return td;
    };

    function editItem(i) {
        imageSlideEditor.show(imageSlideShowPropertyCollection.imageSlideAt(i), function (updatedItem) {
            imageSlideShowPropertyCollection.replaceImageSlide(i, updatedItem);
        });
    };

    function createEditButton(i) {
        return $("<td>").append(ImageSlideShow.EPiServerButtons.EditForm(function () { editItem(i); }));
    };

    function createRemoveButton(i) {
        return $("<td>").append(ImageSlideShow.EPiServerButtons.RemoveForm(function () { removeItem(i); }));
    };

    function createImageSlideNameCell(item) {
        var imageFile = "";
        if (item.ImageUrl != null) {
            var pathArray = item.ImageUrl.split("/");
            imageFile = pathArray[pathArray.length - 1];
        }
        return $("<td>")
            .text(imageFile)
            .attr('title', item.ImageUrl)
            .css({ 'max-width': '100px', 'max-height': '100px', 'width': '100px', 'overflow': 'hidden' });
    };

    function createInformationCell(hasInfo) {
        var cell = $("<td>").css({ 'text-align': 'center' }); ;
        if (hasInfo) {
            return cell.text("X");
        }
        return cell;
    }

    function clearSlides() {
        $(slideContainerId).find(".image-slide-table tr")
            .filter(function (i) { return (i > 0); })
            .remove();
    };

    function addNewPopup() {
        imageSlideEditor.show(emptyShow, function (updatedItem) { imageSlideShowPropertyCollection.addImageSlide(updatedItem); });
    };

    function refreshGlobalSettingsJSonFromInterface() {
        var show = imageSlideShowPropertyCollection.getSlideShow();
        var fields = $(globalSettingsContainerId).find("[dto-id]");

        $.each(fields.filter(textAndSelectFields), function () {
            show[$(this).attr("dto-id")] = $(this).val();
        });
        $.each(fields.filter(checkboxes), function () {
            show[$(this).attr("dto-id")] = $(this).is(":checked");
        });

        imageSlideShowPropertyCollection.updateGlobalSettings(show);
    };

    function textAndSelectFields() {
        return $(this).is("select") || $(this).is("input[type=text]");
    }
    function checkboxes() {
        return $(this).is("input[type=checkbox]");
    }

    function addSlides() {
        var repeater = $(slideContainerId).find(".image-slide-table");
        var slides = imageSlideShowPropertyCollection.getSlideShow().Slides;

        $.each(slides, function (i, slide) {
            var tr = $("<tr>")
                .addClass('slide-row')
                .append(createMoveDownButton(slides, i))
                .append(createMoveUpButton(slides, i))
                .append(createImageSlideNameCell(slide))
                .append(createInformationCell(slide.ImageLinkUrl != null && slide.ImageLinkUrl != ""))
                .append(createInformationCell(slide.ImageTooltip != null && slide.ImageTooltip != ""))
                .append(createInformationCell(slide.ThumbnailUrl != null && slide.ThumbnailUrl != ""))
                .append(createEditButton(i))
                .append(createRemoveButton(i));

            repeater.append(tr);
        });
    };

    function updateSlideShowInterface() {
        clearSlides();
        addSlides();
        refreshGlobalSettings();
    };

    function refreshGlobalSettings() {
        var show = imageSlideShowPropertyCollection.getSlideShow();
        var fields = $(globalSettingsContainerId).find("[dto-id]");

        $.each(fields.filter(textAndSelectFields), function () {
            $(this).val(show[$(this).attr("dto-id")]);
        });
        $.each(fields.filter(checkboxes), function () {
            $(this).attr("checked", show[$(this).attr("dto-id")]);
        });
    };

    return {
        newItem: function () {
            addNewPopup();
        },

        updateGlobalSettings: function () {
            refreshGlobalSettingsJSonFromInterface();
        },

        init: function () {
            imageSlideEditor = ImageSlideShow.EPiServerPopup(imageSlideEditorUrl, 407, 370);
            imageSlideShowPropertyCollection = ImageSlideShow.PropertyCollection(slideContainerId, uniqueIdentifier, updateSlideShowInterface);
            imageSlideShowPropertyCollection.setJson(initialShow);
        }
    };
};

ImageSlideShow.FieldCleaning = ImageSlideShow.FieldCleaning || function (uniqueIdentifier) {
    function addFieldCleaning() {
        var settings = $(["#global-slide-show-settings-", uniqueIdentifier].join(''));
        settings.find(".only-digits").change(function () {
            $(this).val($(this).val().replace(/\D/g, ''));
        });
    };

    return {
        init: function () {
            addFieldCleaning();
        }
    };
};
