// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function ($) {

    jQuery(function ($) {

        //Fade in alerts that are marked as show on page load
        if (!$('.alert.show-on-load.fade').hasClass('in')) {
            $('.alert.show-on-load.fade').addClass('in');
        }
    });
    
}());