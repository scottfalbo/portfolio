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

//general contact form toggle on and off
$(function () {
    $('.toggle-contact').click(function () {
        console.log("hello");
        $('#general-contact').toggleClass('general-contact-hide');
    });
});

//button to cancel and close the contact form
$(function () {
    $('#close-popup').click(function () {
        console.log("hello");
        $('#general-contact').addClass('general-contact-hide');
    });
});

//Google maps API callback function
function initMap() {
    //Set the Latitude and Longitude of the Map  
    var myAddress = new google.maps.LatLng(47.660103768145014, -122.35032563073302);
    console.log(myAddress);
    //Create Options or set different Characteristics of Google Map  
    var mapOptions = {
        center: myAddress,
        zoom: 15,
        minZoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    //Display the Google map in the div control with the defined Options  
    var map = new google.maps.Map(document.getElementById("google-map"), mapOptions);

    //Set Marker on the Map  
    var marker = new google.maps.Marker({
        position: myAddress
    });

    marker.setMap(map); 
}