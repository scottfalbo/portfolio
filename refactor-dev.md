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

[Back to README](README.md#refactor-development-log)