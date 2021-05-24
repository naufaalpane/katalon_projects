!function ($) {
    "use strict";

    $.disable_enable_button = {
        disable: function () {
            $(".btn-std").attr('disabled', true);
            //var loading = document.getElementById('progress-loading');
            //loading.style.display = "block";
            popUpProgressShow();
        },

        enable: function () {
            $(".btn-std").attr('disabled', false);
            //var loading = document.getElementById('progress-loading');
            //loading.style.display = "none";
            popUpProgressHide();
        }
    }

} (window.jQuery);
