var XFlow = XFlow || {};

XFlow.EPiServerButtons = XFlow.EPiServerButtons || function () {
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

XFlow.EPiServerPopup = XFlow.EPiServerPopup || function (baseUrl, width, height) {
    function successCallback(returnValue, onCompleteArgs) {
        if (returnValue != null) {
            var item = new Object();
            item.FormId = returnValue.id;
            item.FormName = returnValue.name;
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

            var fullUrl = baseUrl;
            if (state.FormId != null && state.FormId != "") {
                fullUrl = [baseUrl, "&form=", state.FormId].join('');
            }

            var options = { 'width': width, 'height': height, scrollbars: "no" };
            EPi.CreateDialog(fullUrl, successCallback, onCompleteArgs, args, options);
        }
    };
};

XFlow.PropertyCollection = function (containerId, uniqueIdentifier, collectionChangedCallback) {
    function getOrCreateHiddenField() {
        var hfName = ["hidden-updated-items-", uniqueIdentifier].join('');
        var hiddenField = $(containerId).find("input[type='hidden'][name='" + hfName + "']");
        if (hiddenField.length > 0) {
            return hiddenField;
        }
        hiddenField = $("<input type='hidden'></input>").attr({ name: hfName });
        $(containerId).append(hiddenField);
        return hiddenField;
    };

    function items(collection) {
        if (arguments.length == 0) {
            return eval(getOrCreateHiddenField().val());
        } else if (typeof (collection) == typeof ("")) {
            getOrCreateHiddenField().val(collection);
            collectionChangedCallback();
        } else if (typeof (collection) == typeof ([])) {
            getOrCreateHiddenField().val($.toJSON(collection));
            collectionChangedCallback();
        }
    };

    return {
        setJson: function (json) {
            items(json);
        },
        getItems: function () {
            return items();
        },
        itemAt: function (i) {
            return items()[i];
        },
        addItem: function (item) {
            var collection = items();
            collection[collection.length] = item;
            items(collection);
        },
        replaceItem: function (i, item) {
            var copy = items();
            copy[i] = item;
            items(copy);
        },
        deleteItemAt: function (i) {
            var newCollection = $.grep(items(), function (element, j) {
                return (i != j);
            });

            items(newCollection);
        },
        moveItemUp: function (i) {
            var itemsArray = items();
            var copy = itemsArray[i - 1];
            itemsArray[i - 1] = itemsArray[i];
            itemsArray[i] = copy;
            items(itemsArray);
        },
        moveItemDown: function (i) {
            var itemsArray = items();
            var copy = itemsArray[i + 1];
            itemsArray[i + 1] = itemsArray[i];
            itemsArray[i] = copy;
            items(itemsArray);
        }
    };
};

XFlow.EditModeXFormCollection = XFlow.EditModeXFormCollection || function (uniqueIdentifier, xFormEditorUrl, initialItems, emptyItem) {
    var containerId = ["#xform-item-collection-", uniqueIdentifier].join('');
    var xFormItems;
    var xFormEditor;
    
    function removeItem(i) {
        xFormItems.deleteItemAt(i);
    };

    function moveUp(i) {
        xFormItems.moveItemUp(i);
    };

    function createMoveUpButton(items, i) {
        var td = $("<td>");
        var epiMoveUpButton = XFlow.EPiServerButtons.MoveUp(function () { moveUp(i); });
        if (i == 0) {
            epiMoveUpButton.css('visibility', 'hidden');
        }
        td.append(epiMoveUpButton);
        return td;
    };

    function moveDown(i) {
        xFormItems.moveItemDown(i);
    };

    function createMoveDownButton(items, i) {
        var td = $("<td>");
        var epiMoveDownButton = XFlow.EPiServerButtons.MoveDown(function () { moveDown(i); });
        if (i == items.length - 1) {
            epiMoveDownButton.css('visibility', 'hidden');
        }
        td.append(epiMoveDownButton);
        return td;
    };

    function editItem(i) {
        xFormEditor.show(xFormItems.itemAt(i), function (updatedItem) {
            xFormItems.replaceItem(i, updatedItem);
        });
    };

    function createEditButton(i) {
        return $("<td>").append(XFlow.EPiServerButtons.EditForm(function () { editItem(i); }));
    };

    function createRemoveButton(i) {
        return $("<td>").append(XFlow.EPiServerButtons.RemoveForm(function () { removeItem(i); }));
    };

    function createXFormNameCell(item) {
        return $("<td>").text(item.FormName).css({ 'max-width': '100px', 'max-height': '100px' });
    };

    function clearItems() {
        $(containerId).find(".xform-item-table tr")
            .filter(function (i) { return (i > 0); })
            .remove();
    };
    
    function addNewPopup() {
        xFormEditor.show(emptyItem, function (updatedItem) { xFormItems.addItem(updatedItem); });
    };

    function addItems() {
        var repeater = $(containerId).find(".xform-item-table");
        var items = xFormItems.getItems();

        $.each(items, function (i, item) {
            var tr = $("<tr>")
                .append(createMoveDownButton(items, i))
                .append(createMoveUpButton(items, i))
                .append(createXFormNameCell(item))
                .append(createEditButton(i))
                .append(createRemoveButton(i));

            repeater.append(tr);
        });
    };

    function updateListItems() {
        clearItems();
        addItems();
    };

    return {
        newItem: function () {
            addNewPopup();
        },

        init: function () {
            xFormEditor = XFlow.EPiServerPopup(xFormEditorUrl, 675, 450);
            xFormItems = XFlow.PropertyCollection(containerId, uniqueIdentifier, updateListItems);
            xFormItems.setJson(initialItems);
        }
    };
};
