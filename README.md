# scottfalbo.com

## [scottfalbo.com](https://scottfalbo.com)

version 1.0.0

## About the Project

*Version 2 currently in production.*

[Refactor Development Log v2.0](refactor-dev.md)

Portfolio website for my software development projects and artwork.  It is a .Net Core app built in Visual Studio with C# and deployed on Azure.  All of the sites front facing data, including text fields and portfolios, are handled with a built in admin GUI available on each page when authenticated.

There is more information about the languages, technologies, tools, and site architecture below.

<!-- add site photo here -->

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
            Front end built and styled with HTML5 markup and CSS3 style.
        </td>
    </tr>
    <tr>
        <td width=70>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/javascript.png?raw=true" height=40>
            <img src="https://github.com/scottfalbo/shared-readme-assets/blob/main/assets/jQuery.png?raw=true" height=40>
        </td>
        <td>
            Using JavaScript and jQuery for browser side user interactions.
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
            The website is deployed on Azure.  It using AzureSQL for the database, an Azure Key Vault for storing keys and other secret data, and an Azure Storage Blob for storing uploaded images.
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

## Architecture

### Entity Relationship Diagram

![ERD](/assets/erd.png)

## Domain Models

### Index Admin: Update Image

![index update model](/assets/index_photo_update_model.jpg)

### Index Admin: Update Page

![index update model](/assets/index_update_model.jpg)

---

## Change Log

+ ### [Refactor Development Log v2.0](refactor-dev.md)

+ ### [Original Development Log v1.0](development.md#development-log)

---

## Contact

+ Email: Scottfalboart@gmail.com
+ [scottfalbo.com](https://www.scottfalbo.com)
+ [GitHub](https://github.com/scottfalbo)
+ [LinkedIn](https://www.linkedin.com/in/scott-falbo/)

---

## Acknowledgements

+ [Bootstrap](https://getbootstrap.com/)
+ [json2csharp](https://json2csharp.com/)
+ [Microsoft Docs](https://docs.microsoft.com/en-us/)
