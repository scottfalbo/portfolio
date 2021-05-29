'use strict';

//toggles gallery viewer on when a thumbnail is clicked
$(function () {
    $('.gallery-toggle').click(function () {
        $('#gallery-switch').removeClass('show-gallery');
    });
});

//toggles gallery viewer off when X is clicked
$(function () {
    $('.close-gallery').click(function () {
        $('#gallery-switch').addClass('show-gallery');
    });
});

//event listener for the escape key to get to admin entrance
$(function () {
    $('body').keyup(function (e) {
        if (e.originalEvent.code == 'Escape') {
            window.location.href = "/SecretEntrance";
        }
    });
});

//confirmation box from request form
$(function () {
    $('#email-confirm').click(function () {
        $('#request-confirmation').addClass('hide-confirmation');
        $('#request-confirmation').removeClass('show-confirmation');
    });
});

