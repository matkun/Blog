var EmbeddedExamples = {};

EmbeddedExamples.base = function () {

    return {
        init: function () {
            $("#add-text-here").text("This text was placed here using embedded JavaScript.");
        }
    };
} ();
$(document).ready(EmbeddedExamples.base.init);
