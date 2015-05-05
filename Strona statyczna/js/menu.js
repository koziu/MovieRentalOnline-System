$(document).ready(function () {
    $(window).bind('scroll', function () {
        var navHeight = $("header").height();
        if ($(window).scrollTop() > navHeight) {
            $('nav').removeClass('navbar-static-top');
            $('nav').addClass('navbar-fixed-top');
            $("#wypelniacz").height($("#navbar1").height());
        }
        else {
            $('nav').addClass('navbar-static-top');
            $('nav').removeClass('navbar-fixed-top');
            $("#wypelniacz").height(0);
        }
    });
});