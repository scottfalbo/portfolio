# My Portfolio Website

## About the Project
This is my personal portfolio website.  It is currently under construction and only has a main landing page.  More to come!


[scottfalbo.com](https://falboportfolio.azurewebsites.net/)  

---

## Technologies
<img src = "./assets/cSharp.png" height=50>
<img src = "./assets/visualStudio.png" height=50>

---

## Getting Started
+ Live deployment here: [scottfalbo.com](https://falboportfolio.azurewebsites.net/)  

---

## Architecture

ERD for portfolio section, currently under construction.

<img src="assets/erd.png" width="600">

---

## Change Log
+ 03/25/2021
  + Started initial scaffolding and view layouts.
  + `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation`
+ 03/26/2021
  + Brought in the following dependencies:
    + `Microsoft.EntityFrameworkCore.SqlServer`
    + `Microsoft.EntityFrameworkCore.Tools`
    + `NewtonSoft.Json`
    + `Microsoft.AspNetCore.Mvc.NewtonsoftJson`
  + Built a database and `Models` for `Projects`.
  + Seeded the database with default projects.
  + Created an Interface and Repository for performing admin CRUD actions on the database.
  + Injected the admin dependency into the IndexModel and retrieved the saved projects.
  + Displayed the projects in the Index view.
+ 03/30/2021
  + Added breakpoints and style for desktop view.
  + deployed on Azure.
    + [FalboPortfolio.azurewebsites.net/](https://falboportfolio.azurewebsites.net/)
    + Created ERD for upcoming artwork portfolio of the site.
    + Brought in the following dependencies:
      + `Microsoft.AspNetCore.Identity.EntityFrameWorkCore`
      + `Microsoft.AspNet.Identity.Core`
      + `Microsoft.Extensions.Identity.Core`


---

## Contact
+ Email: Scottfalboart@gmail.com
+ [GitHub](https://github.com/scottfalbo)
+ [LinkedIn](https://www.linkedin.com/in/scott-falbo/)

---

## Acknowledgements
+ [Bootstrap nav tutorial](https://www.youtube.com/watch?v=l2dzzuxvmxk)
