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
  + Built out Booking page with a request form with image upload, loading bar, and confirmation popup.  The form also uses a jQuery function to check upload files and ensure they are less than 20mb total.
  + Added code to strip any whitespace from the filename before being uploaded.
+ 8/25/2021
  + Updated the `Project` model to include properties for accordion classes.
  + Created and set a model for technology icons for use in the project portfolio.
    + Created a Json file with a list of technology icons. Used the file to seed image paths into the database.
  + Added an accordion display to the Code page and updated the model and view with the `Project` data.
+ 08/27/2021
  + Added a carousel display to the projects portfolio and subsequent admin UI.  
  + Updated `Project` and `Image` models and created a `ProjectImage` join table to add images to a project.
  + Added methods to `AdminRepository` to add and remove images and technologies from projects.  Made method to retrieve the list of technologies from database.
  + Added a `HomePageProjects` join table and put a list of technologies on the code main page.  Updated the `ProjectTechnology` class with have a display prop.
  + Built a drop down menu of `<select>` elements to toggle display of technologies on and off for code main page and projects.  The drop down toggle is handled with jQuery.
+ 09/01/2021
  + Added a method to the `UploadServices` that uses regex to add the date and time to the end of a file name to ensure they are unique.

[Back to README](README.md#refactor-development-log)