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

        public ScottFalboCodeModel(IAdmin admin, IUploadService upload, PortfolioDbContext context)
        {
            _admin = admin;
            _upload = upload;
            _context = context;
        }

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
                Projects = await _admin.GetProjects();
                Projects.Reverse();
                HomePage = await _admin.GetHomePage("Code");

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

            Projects = await _admin.GetProjects();
            Projects.Reverse();
            HomePage = await _admin.GetHomePage("Code");

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
            HomePage = await _admin.GetHomePage("Code");
            return Redirect("/Code/ScottFalboCode");
        }

        public async Task OnPostUpdateImage(IFormFile file, Project project)
        {
            if (file != null)
            {
                if (Project.FileName != null)
                {
                    await _admin.DeleteBlobImage(Project.FileName);
                }
                await _upload.UpdateProjectImage(file, Project.Id);
            }

            PageToggles.ActiveProjectAdmin = true;
            PageToggles.StayCollapsed = true;

            Projects = await _admin.GetProjects();
            Projects.Reverse();
            HomePage = await _admin.GetHomePage("Code");

            Redirect("/Code/ScottFalboCode");
        }

        public async Task<IActionResult> OnPostUpdateProject()
        {
            if (Project.Description == null)
                Project.Description = (await _admin.GetProject(Project.Id)).Description;
            if (Project.TechSummary == null)
                Project.TechSummary = (await _admin.GetProject(Project.Id)).TechSummary;

            Project project = await _context.Projects.FindAsync(Project.Id);
            project.Title = Project.Title;
            project.AltText = Project.AltText;
            project.Description = Project.Description;
            project.TechSummary = Project.TechSummary;
            project.Display = Project.Display;
            // add icons here
            project.DeployedLink = Project.DeployedLink;
            project.RepoLink = Project.RepoLink;

            await _admin.UpdateProject(project);

            return Redirect("/Code/ScottFalboCode");
        }

        public async Task OnPostNewProject(string title)
        {
            await _admin.CreateProject(title);

            PageToggles.ActiveProjectAdmin = true;
            PageToggles.StayCollapsed = true;

            Projects = await _admin.GetProjects();
            Projects.Reverse();
            HomePage = await _admin.GetHomePage("Code");

            Redirect("/Code/ScottFalboCode");
        }

        public async Task OnPostDeleteProject(int id)
        {
            Redirect("/Code/ScottFalboCode");
        }
    }

    /// <summary>
    /// Project portfolio toggle properties
    /// </summary>
    public class PageToggles
    {
        // project id from form
        public int ProjectId { get; set; }
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
