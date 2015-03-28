var MyProject = MyProject || {};

MyProject.LanguageTool = function () {
    function animate($row, colorClass) {
        var left = $row.position().left;
        var top = $row.position().top;
        var width = $row.css('width');
        var height = $row.css('height');
        var $overlay = $('.overlay');

        $overlay.addClass(colorClass);
        $overlay.css({
            top: top,
            left: left,
            width: width,
            height: height
        });
        $overlay.fadeIn().fadeOut('slow', function () {
            $overlay.removeClass(colorClass);
        });

    };

    function setStatusMessage(statusClass, message) {
        var $messageContainer = $('#status-message');
        $messageContainer.removeClass('success fail').addClass(statusClass);
        $messageContainer.text(message);
    };

    return {
        init: function () {

        },
        setClickListeners: function () {
            var $translationsTable = $('.translations');
            var token = $translationsTable.data("token");

            $translationsTable.on('click', '.update-button', function (event) {
                var $updateButton = $(this);
                var $currentRow = $updateButton.parent('td').parent('tr');
                var url = $translationsTable.data('url');
                var translationId = $currentRow.attr('id');
                var newText = $currentRow.children('td').children('input[type=text]').val();

                event.preventDefault();

                if (translationId == undefined || translationId == '') {
                    setStatusMessage('fail', $translationsTable.data('no-id-error'));
                    animate($currentRow, 'fail');
                    return;
                }

                var formdata = new FormData();
                formdata.append('id', translationId);
                formdata.append('text', newText);

                $.ajax({
                    url: url,
                    type: 'POST',
                    dataType: 'json',
                    data: formdata,
                    contentType: false,
                    processData: false,
                    headers: {
                        'RequestVerificationToken': token
                    }
                })
                .done(function(response) {
                    if (response.Success) {
                        setStatusMessage('success', response.Message);
                        $currentRow.children('td.updated').text(response.Updated);
                        $currentRow.children('td.updated-by').text(response.UpdatedBy);
                        animate($currentRow, 'success');
                        return;
                    }

                    setStatusMessage('fail', response.Message);
                    animate($currentRow, 'fail');
                });
            });
        }
    };
}();
$(document).ready(MyProject.LanguageTool.init);
$(document).ready(MyProject.LanguageTool.setClickListeners);
