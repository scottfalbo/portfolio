# Refactor Development Log

Refactoring the overall structure of the site while adding new content and sections.

+ 7/29/2021
  + starting a new.
  + Started restructuring the HTML and CSS.
  + Replaced the `Tattoo` and `Drawing` database models with a shared `Image` model.  
  + Moved the main page admin controls from the separate admin dash board to a popup module on the index page.
    + Both the button to toggle and actual CRUD UI only render if the user is authenticated.
+ 8/17/2021
  + Created `Gallery` models to create different page galleries.
  + Restructured art page and used a Bootstrap collapsible accordion to display the galleries.
  + Added admin CRUD to the art page for main page info.
+ 8/16/2021
  + Added edit button and popup module for gallery admin.
+ 8/17/2021
  + Built out all gallery admin CRUD methods and forms.
+ 8/21/2021
  + `Install-Package SixLabors.ImageSharp -IncludePrerelease`
  + Added `ResizeImage` method to the `UploadServices` to resize gallery images to a max height of 1900, and to resize and create a separate thumbnail image.
  + Both images are uploaded to the blob with an over loaded `UploadImage()` that takes a `Stream`.
+ 8/22/2021
  + Implemented Bootstrap carousel in the gallery accordion dropdowns in `ScottFalboArt.cshtml`.
+ 8/23/2021
  + Styled the carousels and added jQuery functions to open and close the gallery viewers.
    + `site.js -> openGallery(gallery)` uses a unique class id created from the galleries title to open only the selected gallery.
  + Used jQuery and a `MutationObserver` object to have an eye graphic open and close with gallery sections.
  + Added `CheckFileName` method to `UploadServices` to ensure there are no duplicate files names on image upload.  Uses gallery repeat popup as warning.
  + Finished styling the art page and associated admin.  
  + Added a loading bar for async CRUD actions.
+ 8/24/2021
  + Scaffolded booking page.  Made contact verification popup, a file upload size check using jQuery, and added a loading bar to the form.

[Back to README](README.md#refactor-development-log)