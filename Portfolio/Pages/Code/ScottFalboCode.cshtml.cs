using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Portfolio.Data;
using Portfolio.Email.Models;
using Portfolio.Email.Models.Interface;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Code
{
    public class ScottFalboCodeModel : PageModel
    {
        public IAdmin _admin;
        public IUploadService _upload;
        private readonly PortfolioDbContext _context;
        public IArtAdmin _art;

        public ScottFalboCodeModel(IAdmin admin, IUploadService upload, PortfolioDbContext context, IArtAdmin art)
        {
            _admin = admin;
            _upload = upload;
            _context = context;
            _art = art;
        }

        [BindProperty]
        public List<Project> Projects { get; set; }
        public HomePage HomePage { get; set; }
        [BindProperty]
        public PageToggles PageToggles { get; set; }
        [BindProperty]
        public Project Project { get; set; }


        public async Task OnGet()
        {
            try
            {
                Project = new Project();
                await Refresh();

                PageToggles = new PageToggles()
                {
                    StayCollapsed = true
                };
            }
            catch (Exception)
            {
                RedirectToPage("Opps");
            }
        }

        /// <summary>
        /// Update Code page photo.
        /// </summary>
        /// <param name="file"> new image </param>
        /// <param name="homepage"> homepage object </param>
        public async Task<IActionResult> OnPostUpdatePhoto(IFormFile file, HomePage homepage)
        {
            if (file != null)
            {
                if (homepage.FileName != null)
                {
                    await _admin.DeleteBlobImage(homepage.FileName);
                }
                await _upload.UpdateSelfie(file, homepage.Id);
            }
            await Refresh();
            return Redirect("/Code/ScottFalboCode");
        }

        /// <summary>
        /// Edit Code page object data and save to database
        /// </summary>
        /// <param name="homepage"> homepage object </param>
        public async Task<IActionResult> OnPostUpdatePage(HomePage homepage)
        {
            if (homepage.Intro == null)
                homepage.Intro = (await _admin.GetHomePage(homepage.Page)).Intro;

            HomePage updatedPage = new HomePage()
            {
                Id = homepage.Id,
                Page = homepage.Page,
                Selfie = homepage.Selfie,
                FileName = homepage.FileName,
                Title = homepage.Title,
                Intro = homepage.Intro
            };
            await _admin.UpdateHomePage(updatedPage);
            await Refresh();
            return Redirect("/Code/ScottFalboCode");
        }

        /// <summary>
        /// Adds images to a project with ProjectImage join table.
        /// </summary>
        /// <param name="files"> files from form </param>
        public async Task OnPostAddImages(IFormFile[] files)
        {
            Image image = new Image();
            foreach (var file in files)
            {
                if (file != null)
                {
                    if (await _upload.CheckFileName(file))
                    {
                        image = await _upload.AddProjectImage(file);
                        await _admin.AddImageToProject(PageToggles.ProjectId, image.Id);
                    }
                    else
                    {
                        PageToggles.RepeatProjectTitle = true;
                    }
                }
            }
            PageToggles.ActiveProjectAdmin = true;
            PageToggles.StayCollapsed = true;
            await Refresh();
            Redirect("/Code/ScottFalboCode");
        }

        /// <summary>
        /// Removes an image from a project and deletes the Image and ProjectImage records.
        /// Using BindProperties from view.
        /// </summary>
        public async Task OnPostDeleteImage()
        {
            await _admin.RemoveImageFromProject(PageToggles.ProjectId, PageToggles.ImageId);
            await _art.DeleteImage(PageToggles.ImageId);

            PageToggles.ActiveProjectAdmin = true;

            await Refresh();

            Redirect("/Code/ScottFalboCode");
        }

        /// <summary>
        /// Updates the project record with new data from form.
        /// </summary>
        /// <param name="isChecked"> bool[] of selected technologies </param>
        public async Task<IActionResult> OnPostUpdateProject(int[] isChecked)
        {
            if (Project.Description == null)
                Project.Description = (await _admin.GetProject(Project.Id)).Description;
            if (Project.TechSummary == null)
                Project.TechSummary = (await _admin.GetProject(Project.Id)).TechSummary;

            Project project = await _context.Projects.FindAsync(Project.Id);
            project.Title = Project.Title;
            project.Description = Project.Description;
            project.TechSummary = Project.TechSummary;
            project.Display = Project.Display;
            project.DeployedLink = Project.DeployedLink;
            project.RepoLink = Project.RepoLink;

            for (int i = 0; i < project.Technologies.Count; i++)
            {
                if (isChecked.Contains(i))
                    project.Technologies[i].Display = true;
                else
                    project.Technologies[i].Display = false;
            }
            await _admin.UpdateProject(project);
            return Redirect("/Code/ScottFalboCode");
        }

        /// <summary>
        /// Creates a new project.  _admin methods create accordion classes and attaches technology list.
        /// </summary>
        /// <param name="title"> string project title from form </param>
        public async Task OnPostNewProject(string title)
        {
            if (!await _admin.CheckProjectTitle(title))
                await _admin.CreateProject(title);
            else
                PageToggles.RepeatProjectTitle = true;

            PageToggles.ActiveProjectAdmin = true;
            PageToggles.StayCollapsed = true;
            await Refresh();
            Redirect("/Code/ScottFalboCode");
        }

        /// <summary>
        /// Deletes a project and removes all associated records from database.
        /// </summary>
        /// <param name="id"> project id </param>
        public async Task OnPostDeleteProject(int id)
        {
            await _admin.DeleteProject(id);
            PageToggles.ActiveProjectAdmin = true;
            PageToggles.StayCollapsed = true;
            await Refresh();
            Redirect("/Code/ScottFalboCode");
        }

        /// <summary>
        /// Updates the list of technologies displayed on the code page from selection input.
        /// </summary>
        /// <param name="isChecked"> bool[] of selected technologies </param>
        public async Task<IActionResult> OnPostUpdateTechnologies(int[] isChecked)
        {
            HomePage page = await _admin.GetHomePage("Code");

            for (int i = 0; i < page.Technologies.Count; i++)
            {
                if (isChecked.Contains(i))
                    page.Technologies[i].Display = true;
                else
                    page.Technologies[i].Display = false;
            }
            await _admin.UpdateHomePage(page);
            return Redirect("/Code/ScottFalboCode"); ;
        }

        /// <summary>
        /// Helper method to assign class properties on task Redirect.
        /// </summary>
        private async Task Refresh()
        {
            Projects = await _admin.GetProjects();
            Projects.Reverse();
            HomePage = await _admin.GetHomePage("Code");
        }
    }

    /// <summary>
    /// Project portfolio toggle properties
    /// </summary>
    public class PageToggles
    {
        // project id from form
        public int ProjectId { get; set; }
        // Image id from form
        public int ImageId { get; set; }
        // bool from form used to toggle project display
        public bool Display { get; set; }
        // bool that is toggled to keep admin window open on refresh
        public bool ActiveProjectAdmin { get; set; }
        // gallery id of active project when images are edited to reopen in place on refresh
        public int ActiveProjectId { get; set; }
        // bool to decide whether or not to keep the projects collapsed on reload
        public bool StayCollapsed { get; set; }
        // bool to throw warning popup if a project title is repeated
        public bool RepeatProjectTitle { get; set; }
    }
}
