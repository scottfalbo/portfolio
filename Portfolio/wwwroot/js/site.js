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
        $('.delete-image-confirm').removeClass('hide-me');
    });
});

// Open confirmation window to delete a gallery
$(function () {
    $('.delete-gallery').click(function () {
        $('.delete-gallery-confirmation').removeClass('hide-me');
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
    console.log(galleryId);
    $('.' + galleryId).removeClass('hide-me');
}

// Close button for gallery viewer.
$(function () {
    $('.close-gallery').click(function () {
        $('.image-gallery').addClass('hide-me');
    });
});








// old code, saving until after refactor is complete

//toggles gallery viewer on when a thumbnail is clicked
// $(function () {
//     $('.gallery-toggle').click(function () {
//         $('#gallery-switch').removeClass('show-gallery');
//     });
// });

// //toggles gallery viewer off when X is clicked
// $(function () {
//     $('.close-gallery').click(function () {
//         $('#gallery-switch').addClass('show-gallery');
//     });
// });

// function getCarouselIndex() {
//     const index = ($('figure.active').index()) + 2;
//     $('#limit').val(index);
//     console.log(index);
// }

// //confirmation box from request form
// $(function () {
//     $('#email-confirm').click(function () {
//         $('#request-confirmation').addClass('hide-confirmation');
//         $('#request-confirmation').removeClass('show-confirmation');
//     });
// });

// //general contact form toggle on and off
// $(function () {
//     $('.toggle-contact').click(function () {
//         $('#general-contact').toggleClass('general-contact-hide');
//     });
// });

// //button to cancel and close the contact form
// $(function () {
//     $('#close-popup').click(function () {
//         $('#general-contact').addClass('general-contact-hide');
//     });
// });

// //buttons to hide and show studio page pop ups.
// // aftercare button
// $(function () {
//     $('#show-policies').click(function () {
//         $('#shop-policies').removeClass('hidden-popup');
//     });
// });
// // policies button
// $(function () {
//     $('#show-aftercare').click(function () {
//         $('#tattoo-aftercare').removeClass('hidden-popup');
//     });
// });
// // close the popup window
// $(function () {
//     $('.studio-popup-close').click(function () {
//         $('.studio-popup').addClass("hidden-popup");
//     })
// })


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

// // Request form upload file size checker
// $(function () {
//     $('.request-upload').on('change', (e) => {
//         let uploadSize = 0;
//         let files = e.currentTarget.files;
//         Array.from(files).forEach((file) => {
//             uploadSize += (file.size/1000000);
//         });
//         if (uploadSize > 20) {
//             $('.request-upload').val('');
//             $('.upload-too-large').removeClass("hidden-popup");
//         }
//     });
// });

// // Close request upload size warning
// $(function () {
//     $('.upload-too-large-close').click(function () {
//         console.log('hello');
//         $('.upload-too-large').addClass("hidden-popup");
//     });
// });