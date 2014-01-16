var SampleCode = {};

SampleCode.BookmarkLinks = function () {
    var currentPageId;

    function removeBookmarkComponents() {
        var selector = $("#ddlBookmarkSelectorContainer");
        if (selector.length) {
            selector.remove();
        }
        var err = $("#error-msg");
        if(err.length) {
            err.remove();
        }
    }

    function bookmarkOrDefault() {
        var url = decodeURIComponent(window.location.href);
        if (!url) {
            return "";
        }
        var split = url.split('#');
        if (split.length <= 1) {
            return "";
        }
        return split[1];
    }

    function createBookmarkSelector() {
        $.ajax({
            type: "GET",
            url: "/CodeSample/BookmarkLinks/AvailableBookmarks.ashx",
            data: {
                pageId: currentPageId,
                bookmark: bookmarkOrDefault()
            },
            dataType: 'html',
            timeout: 10000, // Better to get the DDL late than not at all.
            success: function (data) {
                removeBookmarkComponents();
                $("#linkInternalAddressContainer").append(data);
            },
            error: function (e, xhr, exception) {
                removeBookmarkComponents();
                var error = $("<div id='error-msg' class='error'>ERROR: Unable to locate bookmarks for page with id='" + currentPageId + "'.</p>");
                $("#linkInternalAddressContainer").append(error);
            }
        });
    }

    function pageSelectorCallbackHandler() {
        var newPageId = $("input[id$='linkinternalurl_Value']").val();

        if (newPageId == currentPageId) {
            return;
        }
        if (newPageId == "") {
            removeBookmarkComponents();
            return;
        }
        currentPageId = newPageId;
        createBookmarkSelector();
    }

    function injectCallback() {
        // Inserts callback function by replacing the parameter in EPiServer's onclick call; old one was 'null'.
        var pageSelectButton = $("#linkInternalAddressContainer input[type=button].epismallbutton");
        var onClickAttribute = pageSelectButton.attr("onclick");
        var onClickAttrArr = onClickAttribute.split(',');
        onClickAttrArr[7] = "SampleCode.BookmarkLinks.pageSelectorCallback";
        onClickAttribute = onClickAttrArr.join(',');
		
        // We cannot put it back in the onclick attribute since IE won't accept it, and nothing will happen when
        // the button is pushed. globalEval here should be as dangerous as if someone changed the vanilla onclick
        // string with FireBug.
        pageSelectButton.removeAttr('onclick');
        pageSelectButton.on('click', function () {
            $.globalEval(onClickAttribute);
        });
    }

    return {
        pageSelectorCallback: function () {
            pageSelectorCallbackHandler();
        },
        init: function () {
            currentPageId = $("input[id$='linkinternalurl_Value']").val();
            
            // Initialize hash tag selector if there is a page selected.
            if (!isNaN(currentPageId)) { 
                createBookmarkSelector();
            }
            
            injectCallback();
        }
    };
}();
$(document).ready(SampleCode.BookmarkLinks.init);
