'use strict';

// Open button for admin popup modules. 
$(function () {
    $('.open-admin').click(function () {
        $('.popup-module-outer').removeClass('hide-me');
    });
});

// Open button for gallery admin popup module. 
$(function () {
    $('.open-gallery-admin').click(function () {
        $('.gallery-module-outer').removeClass('hide-me');
    });
});

// Close button for popup modules. 
$(function () {
    $('.module-close-button').click(function () {
        $('.fullscreen').addClass('hide-me');
        $('.delete-image-confirm').addClass('hide-me');
        $('.delete-gallery-confirmation').addClass('hide-me');
    });
});


// Open confirmation window to delete a gallery image
$(function () {
    $('.delete-gallery-image').click(function () {
        $(this).siblings('.delete-image-confirm')
            .removeClass('hide-me');
    });
});

// Open confirmation window to delete a gallery
$(function () {
    $('.delete-gallery').click(function () {
        $(this).siblings('.delete-gallery-confirmation')
            .removeClass('hide-me');
    });
});

// Cancel deletion confirmation
$(function () {
    $('.cancel-confirmation').click(function () {
        $('.confirm-popup').addClass('hide-me');
    });
});

// Close gallery title repeat popup
$(function () {
    $('.repeat-gallery-close').click(function () {
        $('.repeat-gallery-title').addClass('hide-me');
    });
});

// //event listener for the escape key to get to admin entrance
$(function () {
    $('body').keyup(function (e) {
        if (e.originalEvent.code == 'Escape') {
            $.ajax({
                url: '/Shared/LoggedIn',
            })
                .done(function (result) {
                    if (result == "true") {
                        window.location.href = "/Index";
                    }
                    else {
                        window.location.href = "/SecretEntrance";
                    }
                });
        }
    });
});

// Open button (thumbnail) for each galleries respective viewer.
// Each gallery uses a unique class generated from the gallery title.
function openGallery(gallery) {
    var galleryId = $(gallery).data('gallery-id');
    $('.' + galleryId).removeClass('hide-me');
}

// Close button for gallery viewer.
$(function () {
    $('.close-gallery').click(function () {
        $('.image-gallery').addClass('hide-me');
    });
});

// -------------------------------------------- Gallery eyes
// Open the active galleries eye.
function openEye(gallery) {
    $('.' + gallery).addClass('open-gallery-eye');
}

// Checks for open gallery on page load.
$(document).ready(function () {
    checkActiveGallery();
});

let galleryList = [];

// Checks each gallery and turns on eye if classList contains 'show'.
function checkActiveGallery() {
    galleryList.forEach(x => {
        const gallery = document.getElementById(x);
        if (gallery.classList.contains('show')) {
            openEye(gallery.id + '_eye');
        }
    });
}

// Callback function for the observer event listener.
function checkChange(mutations) {
    $('.gallery-eye').removeClass('open-gallery-eye');
    checkActiveGallery();
}

// MutationObserver instantiation and configuration.
var config = {
    attributes: true
};
var targets = document.getElementsByClassName('collapse');
let observer = new MutationObserver(checkChange);

$('.collapse').each(function () {
    observer.observe(this, config);
})

// Loading window toggle
$(function () {
    $('.loader').click(function () {
        $('#loading-bar-outer').removeClass('hide-me');
    })
})

// Request form upload file size checker
$(function () {
    $('.request-upload').on('change', (e) => {
        let uploadSize = 0;
        let files = e.currentTarget.files;
        Array.from(files).forEach((file) => {
            uploadSize += (file.size / 1000000);
        });
        if (uploadSize > 20) {
            $('.request-upload').val('');
            $('.upload-too-large').removeClass("hide-me");
        }
    });
});

// Close request upload size warning
$(function () {
    $('.upload-too-large-close').click(function () {
        $('.upload-too-large').addClass("hide-me");
    });
});

// Close request verification window.
$(function () {
    $('#email-confirm').click(function () {
        $('#request-confirmation').addClass('hide-me');
    });
});

// Nav button mouse over tooltip.
$(document).ready(function () {
    $('.site-nav-button').mouseenter(function () {
        $(this).children('.mouse-over-tooltip').removeClass('hide-me');
    });
    $('.site-nav-button').mouseleave(function () {
        $(this).children('.mouse-over-tooltip').addClass('hide-me');
    });
});

$(document).on('mouseover', function (e) {
    $('.mouse-over-tooltip').css({
        left: e.pageX,
        top: e.pageY
    });
});

$(document).ready(function () {
    console.log('what what');
    $('.technology-dropdown').slideToggle();
});

$(function () {
    $('.technologies-menu-button').click(function () {
        $('.technology-dropdown').slideToggle();
    });
});

//pagination stuff
// function getCarouselIndex() {
//     const index = ($('figure.active').index()) + 2;
//     $('#limit').val(index);
//     console.log(index);
// }




//------------------------ Google Map API stuff

// //https://developers.google.com/maps/documentation/javascript/examples/style-array
// //Google maps API callback function
// function initMap() {

//     var myAddress = new google.maps.LatLng(47.660103768145014, -122.35032563073302);

//     var mapOptions = {
//         center: myAddress,
//         zoom: 10,
//         minZoom: 10,
//         mapTypeId: google.maps.MapTypeId.ROADMAP,
//         //https://developers.google.com/maps/documentation/javascript/examples/style-array
//         styles: [
//             { elementType: "geometry", stylers: [{ color: "#242f3e" }] },
//             { elementType: "labels.text.stroke", stylers: [{ color: "#242f3e" }] },
//             { elementType: "labels.text.fill", stylers: [{ color: "#746855" }] },
//             {
//                 featureType: "administrative.locality",
//                 elementType: "labels.text.fill",
//                 stylers: [{ color: "#d59563" }],
//             },
//             {
//                 featureType: "poi",
//                 elementType: "labels.text.fill",
//                 stylers: [{ color: "#d59563" }],
//             },
//             {
//                 featureType: "poi.park",
//                 elementType: "geometry",
//                 stylers: [{ color: "#263c3f" }],
//             },
//             {
//                 featureType: "poi.park",
//                 elementType: "labels.text.fill",
//                 stylers: [{ color: "#6b9a76" }],
//             },
//             {
//                 featureType: "road",
//                 elementType: "geometry",
//                 stylers: [{ color: "#38414e" }],
//             },
//             {
//                 featureType: "road",
//                 elementType: "geometry.stroke",
//                 stylers: [{ color: "#212a37" }],
//             },
//             {
//                 featureType: "road",
//                 elementType: "labels.text.fill",
//                 stylers: [{ color: "#9ca5b3" }],
//             },
//             {
//                 featureType: "road.highway",
//                 elementType: "geometry",
//                 stylers: [{ color: "#746855" }],
//             },
//             {
//                 featureType: "road.highway",
//                 elementType: "geometry.stroke",
//                 stylers: [{ color: "#1f2835" }],
//             },
//             {
//                 featureType: "road.highway",
//                 elementType: "labels.text.fill",
//                 stylers: [{ color: "#f3d19c" }],
//             },
//             {
//                 featureType: "transit",
//                 elementType: "geometry",
//                 stylers: [{ color: "#2f3948" }],
//             },
//             {
//                 featureType: "transit.station",
//                 elementType: "labels.text.fill",
//                 stylers: [{ color: "#d59563" }],
//             },
//             {
//                 featureType: "water",
//                 elementType: "geometry",
//                 stylers: [{ color: "#17263c" }],
//             },
//             {
//                 featureType: "water",
//                 elementType: "labels.text.fill",
//                 stylers: [{ color: "#515c6d" }],
//             },
//             {
//                 featureType: "water",
//                 elementType: "labels.text.stroke",
//                 stylers: [{ color: "#17263c" }],
//             },
//         ],
//     };

//     var map = new google.maps.Map(document.getElementById("google-map"), mapOptions);

//     var marker = new google.maps.Marker({
//         position: myAddress
//     });

//     marker.setMap(map); 
// }

