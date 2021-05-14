'use strict';

$(function () {
    $('.gallery-toggle').click(function () {
        $('#gallery-switch').removeClass('show-gallery');
    });
});

$(function () {
    $('.close-gallery').click(function () {
        $('#gallery-switch').addClass('show-gallery');
    });
});


