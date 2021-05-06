using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Admin
{
    public class ProjectLairModel : PageModel
    {
        public IAdmin _adminContext;
        public IUploadService _uploadService;

        public ProjectLairModel(IAdmin context, IUploadService upload)
        {
            _adminContext = context;
            _uploadService = upload;
        }

        public List<Project> ProjectList { get; set; }

        [BindProperty]
        public Project Project { get; set; }

        [BindProperty]
        public string ImageUri { get; set; }

        public async Task OnGet()
        {
            try
            {
                ProjectList = await _adminContext.GetProjects();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }
        }

        public async Task<IActionResult> OnPost (Project project)
        {
            Project newProject = new Project()
            {
                Title = Project.Title,
                Order = Project.Order,
                SourceURL = Project.SourceURL,
                AltText = Project.AltText,
                Description = Project.Description,
                RepoLink = Project.RepoLink,
                DeployedLink = Project.DeployedLink
            };
            await _adminContext.CreateProject(newProject);
            return Redirect("/Admin/ProjectLair");
        }

        /// <summary>
        /// Update a projects saved data
        /// </summary>
        /// <param name="project"> Project object from the form </param>
        /// <returns> updates DB and redirects in place </returns>
        public async Task<IActionResult> OnPostEdit (Project project)
        {
            if (project.Description == null)
                Project.Description = (await _adminContext.GetProject(project.Id)).Description;

            Project updatedProject = new Project()
            {
                Id = Project.Id,
                Title = Project.Title,
                Order = Project.Order,
                SourceURL = Project.SourceURL,
                AltText = Project.AltText,
                Description = Project.Description,
                RepoLink = Project.RepoLink,
                DeployedLink = Project.DeployedLink
            };
            await _adminContext.UpdateProject(updatedProject);
            return Redirect("/Admin/ProjectLair");
        }

        /// <summary>
        /// Delete a project from the database
        /// </summary>
        /// <returns> updates DB and redirects in place </returns>
        public async Task<IActionResult> OnPostDelete()
        {
            await _adminContext.DeleteProject(Project.Id);
            return Redirect("/Admin/ProjectLair");
        }

        public async Task<IActionResult> OnPostAddImage(IFormFile file)
        {
            await _uploadService.AddProjectImage(file);
            return Redirect("/Admin/ProjectLair");
        }

        public async Task<IActionResult> OnPostUpdateImage(IFormFile file)
        {
            await _adminContext.DeleteBlobImage(Project.FileName);
            await _uploadService.UpdateImage(file, Project.Id);

            return Redirect("/Admin/ProjectLair");
        }

    }
}
