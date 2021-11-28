# scottfalbo.com

Deployed on Azure at [scottfalbo.com](https://scottfalbo.com)

version 2.0.0

---

+ **Contents**
  + [About the Project](#about-the-project)
  + [Technologies](#technologies)
  + [Entity Relationship Diagram](#entity-relationship-diagram)
  + [Domain Models](#domain-models)
  + [Change Log](#change-log)
  + [Contact](#contact)
  + [Acknowledgements](#acknowledgements)

---

## About the Project

Portfolio website for my software development projects and artwork.  It is a .Net Core app built in Visual Studio with C# and deployed on Azure.  All of the sites front facing data, including text fields and portfolios, are handled with a built in admin GUI available on each page when authenticated.

There is more information about the languages, technologies, tools, and site architecture below.

![index page screenshot](/assets/screenshots/index.jpg)

---

## Technologies

<table>
    <tr>
        <td width=50>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/dotnetcore.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/cSharp.png?raw=true" height=40>
        </td>
        <td>
            This is a .Net Core project written in C# using Razor pages.  Login and authentication is handled by Microsoft Identity.
        </td>
    </tr>
    <tr>
        <td width=70>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/html.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/css.png?raw=true" height=40>
        </td>
        <td>
            Front end built and styled with HTML5 markup and CSS3 style. The site is fully responsive with mobile/tablet and laptop/desktop break points.
        </td>
    </tr>
    <tr>
        <td width=70>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/javascript.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/jQuery.png?raw=true" height=40>
        </td>
        <td>
            JavaScript and jQuery are used for browser side user interactions.
        </td>
    </tr>
    <tr>
        <td width=70>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/azure.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/azure_sql.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/key_vault.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/azureBlob.png?raw=true" height=40>
        </td>
        <td>
            The website is deployed on Azure, using AzureSQL for the database, an Azure Key Vault for storing keys and other secret data, and an Azure Storage Blob for storing uploaded images.
        </td>
    </tr>
    <tr>
        <td width=70>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/bootstrap.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/sendgrid.png?raw=true" height=40>
        </td>
        <td>
            I used Bootstrap for the main navigation drop down menus and breakpoint, the collapsing accordion galleries, and the carousel image viewers. The request form on the Art/Booking page uses Twilio SendGrid to email the form.
        </td>
    </tr>
    <tr>
        <td width=70>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/git.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/github-light.png?raw=true" height=40>
        </td>
        <td>
            Git and GitHub used for version control.
        </td>
    </tr>
    <tr>
        <td width=70>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/visual_studio.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/vscode.png?raw=true" height=40>
        </td>
        <td>
            C# and HTML written in Visual Studio.  JavaScript, CSS and, Markdown written in VS Code.
        </td>
    </tr>
    <tr>
        <td width=70>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/photoshop.png?raw=true" height=40>
        </td>
        <td>
            Image assets created and/or edited with Adobe Photoshop.
        </td>
    </tr>
</table>

---

## Entity Relationship Diagram

+ `HomePage` 1:many `HomePageTechnology` many:1 `Technology`
+ `Project` 1:many `ProjectTechnology` many:1 `Technology`
+ `Project` 1:many `ProjectImage` many:1 `Image`
+ `Gallery` 1:many `GalleryImage` many:1 `Image`

![ERD](/assets/erd.png)

---

## Domain Models

Each page on the site has it's own popup admin GUI, visible to authenticated users, that controls the CRUD actions for its content.

### Index Admin

The index page admin controls the home page's profile image, title, and intro paragraph.  

*Index admin screenshot*

![index admin screenshot](/assets/screenshots/index_admin.jpg)

#### Update Image

This form updates the profile photos on each page. It removes the previous file from the blob, uploads the new one, and creates a new record in the database.

![index update model](/assets/index_photo_update_model.jpg)

#### Update Page

This form updates the general information on each page.

![index update model](/assets/index_update_model.jpg)

---

### Code Page Admin

The code page has three separate admin panels.  One for the basic page information, which is the same pattern as the index admin.  The second is an admin GUI with a dropdown `<select>` menu containing a broad list of languages and technologies.  The selected icons appear on the page.  
The third GUI is what controls the portfolio section of the page.  It has CRUD actions to create, update, and delete projects, as well as add and remove images from projects.
I've included domain models for the add and remove projects methods. The general page admin is same as the index page. The add and remove image methods will be detailed below under the Art Page section.

*Project admin screenshot*

![project admin screenshot](/assets/screenshots/project_admin.jpg)

#### Add Project

The add project form takes in a new project title.  Because the title will be used for class names to control the Bootstrap accordion dropdown they need to be unique.  The `CheckProjectTitle` method validates the input.  When the project is created the title is normalized and used to assign several class names for use with dropdowns.  The project is saved to the database with the title and classnames.  Then a list of technologies is attached to the project for display in portfolio view.

![add project model](/assets/code_project_add.jpg)

#### Delete Project

The delete project form sends the project Id to the model. First the join table records for the ProjectImages are removed. Then the respective Image records are removed and the file is deleted from blob storage. Lastly the ProjectTechnology records are removed and then the Project record.

![delete project model](/assets/code_project_delete.jpg)

---

### Art Page Admin

The art page has an admin for the general page content just like the index and code page. The galleries have their own GUI that can add and remove galleries, similar to adding and removing projects from the code page, and add and remove images from galleries.  

*Gallery admin screenshot*

![gallery admin screenshot](/assets/screenshots/gallery_admin.jpg)

#### Add Images

The add images form sends an array of IFormFiles to the AddImages handler. Each image is resized for gallery view and a copy is made and resized for thumbnail view.  Both file names have the white space stripped and the date and time attached to ensure a unique name in the blob.  Once stored in the blob the filename and blob.uris are used to create and save an Image in the database. Then the image is added to the gallery with an `ImageGallery` join table and the page is refreshed.

![add image model](/assets/gallery_add_image.jpg)

#### Delete Image

The delete image form removes the image from the gallery by deleting the `GalleryImage` join table record.  Then the image and thumbnail are removed from the blob by filename.  And finally the image record is removed from the database.

![delete image model](/assets/gallery_delete_image.jpg)

---

## Change Log

+ *11/28/2021* : Removed the Booking page from the Art section of the site.

+ [Refactor Development Log v2.0](refactor-dev.md)

+ [Original Development Log v1.0](development.md#development-log)

---

## Contact

+ Email: Scottfalboart@gmail.com
+ [scottfalbo.com](https://www.scottfalbo.com)
+ [GitHub](https://github.com/scottfalbo)
+ [LinkedIn](https://www.linkedin.com/in/scott-falbo/)

---

## Acknowledgements

+ [Bootstrap](https://getbootstrap.com/)
+ [Microsoft Docs](https://docs.microsoft.com/en-us/)
+ Google and Stack Overflow
