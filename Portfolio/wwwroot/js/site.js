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

    var myAddress = new google.maps.LatLng(47.660103768145014, -122.35032563073302);
 
    var mapOptions = {
        center: myAddress,
        zoom: 10,
        minZoom: 10,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        //https://developers.google.com/maps/documentation/javascript/examples/style-array
        styles: [
            { elementType: "geometry", stylers: [{ color: "#242f3e" }] },
            { elementType: "labels.text.stroke", stylers: [{ color: "#242f3e" }] },
            { elementType: "labels.text.fill", stylers: [{ color: "#746855" }] },
            {
                featureType: "administrative.locality",
                elementType: "labels.text.fill",
                stylers: [{ color: "#d59563" }],
            },
            {
                featureType: "poi",
                elementType: "labels.text.fill",
                stylers: [{ color: "#d59563" }],
            },
            {
                featureType: "poi.park",
                elementType: "geometry",
                stylers: [{ color: "#263c3f" }],
            },
            {
                featureType: "poi.park",
                elementType: "labels.text.fill",
                stylers: [{ color: "#6b9a76" }],
            },
            {
                featureType: "road",
                elementType: "geometry",
                stylers: [{ color: "#38414e" }],
            },
            {
                featureType: "road",
                elementType: "geometry.stroke",
                stylers: [{ color: "#212a37" }],
            },
            {
                featureType: "road",
                elementType: "labels.text.fill",
                stylers: [{ color: "#9ca5b3" }],
            },
            {
                featureType: "road.highway",
                elementType: "geometry",
                stylers: [{ color: "#746855" }],
            },
            {
                featureType: "road.highway",
                elementType: "geometry.stroke",
                stylers: [{ color: "#1f2835" }],
            },
            {
                featureType: "road.highway",
                elementType: "labels.text.fill",
                stylers: [{ color: "#f3d19c" }],
            },
            {
                featureType: "transit",
                elementType: "geometry",
                stylers: [{ color: "#2f3948" }],
            },
            {
                featureType: "transit.station",
                elementType: "labels.text.fill",
                stylers: [{ color: "#d59563" }],
            },
            {
                featureType: "water",
                elementType: "geometry",
                stylers: [{ color: "#17263c" }],
            },
            {
                featureType: "water",
                elementType: "labels.text.fill",
                stylers: [{ color: "#515c6d" }],
            },
            {
                featureType: "water",
                elementType: "labels.text.stroke",
                stylers: [{ color: "#17263c" }],
            },
        ],
    };

    var map = new google.maps.Map(document.getElementById("google-map"), mapOptions);

    var marker = new google.maps.Marker({
        position: myAddress
    });

    marker.setMap(map); 
}