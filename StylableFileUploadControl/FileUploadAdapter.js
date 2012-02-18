var CodeSample = CodeSample || {};

CodeSample.fileUploadAdapter = function () {

    function bindFileSelectionDialogTriggers() {
        var containers = $("div.file-upload");
        var clickables = containers.find('input[type="text"]')
                                    .add(containers.find('input[type="button"]'));
        clickables.click(function () {
            $(this).siblings('input[type="file"]').trigger('click');
        });
    };

    function bindTextBoxUpdaters() {
        var containers = $("div.file-upload");
        containers.find('input[type="file"]').change(function () {
            $(this).siblings('input[type="text"]').val($(this).val());
        });
    };

    return {
        init: function () {
            bindFileSelectionDialogTriggers();
            bindTextBoxUpdaters();
        }
    };
} ();
$(document).ready(CodeSample.fileUploadAdapter.init);