<img src="./assets/git_title.png">

# [www.scottfalbo.com](https://www.scottfalbo.com)

version 1.0.0



## About the Project

This is my personal portfolio page for both coding projects and artwork.  It is a .NET Core application built in Visual Studio with C# and deployed on Azure.

The galleries, as well as text areas, are all stored in an AzureSQL Database.  All areas of the site can be updated via an admin dashboard with full CRUD actions.  

Gallery images are stored in an Azure Blob, all uploading and removal are handled via the admin dashboard.

All of the site contact forms utilize SendGrid.  The toggle controls to show and hide forms are handled by jQuery.

My current Instagram feed is displayed on the Art Page using the FaceBook Developers Instagram API.

All API keys, access tokens and other site secrets are stored in an Azure Key Vault.

---

## Technologies

<table border>
  <tr align=center>
    <td width=80>
      <img src = "./assets/cSharp.png" height=50> 
    </td>
    <td width=80>
      <img src = "./assets/dotnetcore.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/visualStudio.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/azure.png" height=50>
    </td>
  </tr>
    <tr>
    <td align=center>
      C#
    </td>
    <td align=center>
      .NET Core
    </td>
    <td align=center>
      Visual Studio
    </td>
    <td align=center>
      Azure
    </td>
  </tr>
  <tr align=center>
    <td width=80>
      <img src = "./assets/html.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/css.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/bootstrap.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/github-light.png" height=50>
    </td>
  </tr>
    <tr>
    <td align=center>
      HTML 5
    </td>
    <td align=center>
      CSS3
    </td>
    <td align=center>
      Bootstrap
    </td>
    <td align=center>
      GitHub
    </td>
  </tr>
  </tr>
  <tr align=center>
    <td width=80>
      <img src = "./assets/azure_sql.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/azureBlob.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/key_vault.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/facebook_dev.png" height=50>
    </td>
  </tr>
    <tr>
    <td align=center>
      AzureSQL
    </td>
    <td align=center>
      Azure Blob
    </td>
    <td align=center>
      Azure Key Vault
    </td>
    <td align=center>
      Facebook Dev
    </td>
  </tr>
  <tr align=center>
    <td width=80>
      <img src = "./assets/sendgrid.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/jQuery.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/vscode.png" height=50>
    </td>
    <td width=80>
      <img src = "./assets/photoshop.png" height=50>
    </td>
  </tr>
    <tr>
    <td align=center>
      SendGrid
    </td>
    <td align=center>
      jQuery
    </td>
    <td align=center>
      Visual Studio Code
    </td>
    <td align=center>
      Adobe Photoshop
    </td>
  </tr>
</table>

---

## Change Log

+ 06/20/2021
  + **version 1.0.0**
    + Initial deployment of full site
+ 06/21/2021
  + Fixed a handful of style issues across the site.
  + Used jQuery ajax to call a method to see if the user is authenticated. If user is not logged in the secret entrance appears.  If the user is logged in the secret lair appear.  That's what I call my dashboard.
+ 06/23/2021
  + Added screaming Ethel favicon.
+ 07/10/2021
  + Added an image upload input to the request form.
    + Updated SendGridTemplate object to include the URIs of the uploaded images.
  + Changed all image upload inputs to accept multiple files.
    + Added a jQuery function to enforce a max upload size of 20mbs.  
  + Added Google ReCaptcha v3 to the contact forms.
    + [Implementation docs](https://developers.google.com/recaptcha/docs/v3)

+ ### [Development Log](development.md#development-log)

---

## Contact

+ Email: Scottfalboart@gmail.com
+ [scottfalbo.com](https://www.scottfalbo.com)
+ [GitHub](https://github.com/scottfalbo)
+ [LinkedIn](https://www.linkedin.com/in/scott-falbo/)

---

## Acknowledgements

+ [Bootstrap Carousel](https://getbootstrap.com/docs/4.0/components/carousel/)
+ [json2csharp](https://json2csharp.com/)
+ [Microsoft Docs](https://docs.microsoft.com/en-us/)
+ [Google Maps dark mode script](https://developers.google.com/maps/documentation/javascript/examples/style-array)
