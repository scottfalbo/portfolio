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

$(function () {
    $('body').keyup(function (e) {
        if (e.originalEvent.code == 'Escape')
        {
            window.location.href = "./SecretEntrance";
        }
    });
})

