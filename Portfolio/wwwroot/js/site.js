'use strict';

$(function () {
    $('.gallery-toggle').click(function () {
        $('#tattoo-gallery').removeClass('show-gallery');
    });
});

$(function () {
    $('.close-gallery').click(function () {
        $('#tattoo-gallery').addClass('show-gallery');
    });
});


