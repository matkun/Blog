var ImageSelector = ImageSelector || {};

ImageSelector.Popup = ImageSelector.Popup || function (uniqueIdentifier) {
    var imageSelectorField = $(["#image-selector-", uniqueIdentifier].join(''));

    function epiFileManagerDialog(selectedFile, onCloseCallbackFunction) {
        var onCompleteArguments = new Object();
        var dialogArguments = new Object();
        dialogArguments.hideBookmarks = true;
        dialogArguments.parentWindow = document.window;
        var fileManagerBrowserUrl = EPi.ResolveUrlFromUI("edit/FileManagerBrowser.aspx") + "?selectedfile=" + selectedFile;
        EPi.CreateDialog(fileManagerBrowserUrl, onCloseCallbackFunction, onCompleteArguments, dialogArguments, { width: 660, height: 460, scrollbars: "no" });
    };
    
    function selectFile() {
        epiFileManagerDialog(imageSelectorField.val(), function (a, b) {
            if (a.closeAction == 'insert') {
                var selectedFilePath = a.items[0].path;
                imageSelectorField.val(selectedFilePath);
                EPi.PageLeaveCheck.SetPageChanged(true);
            } else if (a.closeAction == 'clear') {
                imageSelectorField.val('');
                EPi.PageLeaveCheck.SetPageChanged(true);
            } else if (a.closeAction == 'cancel') {
                // ..
            }
        });
    };

    function initialize() {
        var imageSelectorButton = $(["#image-selector-button-", uniqueIdentifier].join(''));
        imageSelectorButton.click(function () { selectFile(); });
    };

    return {
        init: function () {
            initialize();
        }
    };
};
