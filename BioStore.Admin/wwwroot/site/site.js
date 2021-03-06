
$(document).ready(function () {
    $(document).on({
        ajaxStart: function () { $(".preloader-it").show(); },
        ajaxStop: function () { $(".preloader-it").fadeOut("slow"); }
    });    
});