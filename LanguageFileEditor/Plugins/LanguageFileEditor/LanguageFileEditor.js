$(document).ready(function () {
    $('div.nodeStart a').click(function () {
        if ($(this).text() == "-") {
            $(this).text("+");
        } else {
            $(this).text("-");
        }
        $(this).parent().next('div.toggleBlock').toggle();
        return false;
    });

    var contentDictionary = new Object();
    $('#EditArea input').change(function () {
        var textBox = $(this);
        contentDictionary[textBox.attr('title')] = textBox.val();
        textBox.parent('div').addClass("hasChanged");
    });

    $('input.saveFileButton').click(function () {
        var targetFilename = $('#LanguageFileEditor #ControlArea input[type="text"]').val();
        if (targetFilename == "") {
            $('span.errorMessage').removeAttr("style").addClass("visible");
            return;
        }
        if (targetFilename.match(".xml$") != ".xml") {
            targetFilename += ".xml";
        }
        var data = new Object();
        data["targetFilename"] = targetFilename;
        data["patternFilename"] = $('#LanguageFileEditor input[type="hidden"]').val();
        data["xmlContent"] = contentDictionary;
        var jsonData = JSON.stringify(data);

        $.ajax({
            type: "POST",
            url: "/Plugins/LanguageFileEditor/UpdateLanguageFile.aspx",
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('span#Message').addClass("show").text("Changes saved successfully to file " + targetFilename + ".");
                $('#ControlArea input.backButton').addClass("show").removeClass("hide");
                $('#ControlArea input.cancelButton').addClass("hide").removeClass("show");
            },
            error: function (response) {
                $('span#Message').addClass("show").text("Unsuccessful! Nothing saved: " + response.status + " " + response.statusText);
            }
        });
    });
});
