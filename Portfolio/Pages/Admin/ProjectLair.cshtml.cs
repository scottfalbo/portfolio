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

        /// <summary>
        /// Gets a list of all Project objects in the database
        /// </summary>
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
                ImageUrl = Project.ImageUrl,
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

        /// <summary>
        /// Uploads image to azure blob
        /// Instantiates and saves a new project object with image source url
        /// </summary>
        /// <param name="file"> input file </param>
        /// <returns> redirect in place </returns>
        public async Task<IActionResult> OnPostAddImage(IFormFile file)
        {
            if (file != null)
                await _uploadService.AddProjectImage(file);
            return Redirect("/Admin/ProjectLair");
        }

        /// <summary>
        /// Uploads the new image, updates the project in the database
        /// Deletes old image from azure blob storage
        /// </summary>
        /// <param name="file"> input file </param>
        /// <returns> redirects in place </returns>
        public async Task<IActionResult> OnPostUpdateImage(IFormFile file)
        {
            if (file != null)
            {
                await _adminContext.DeleteBlobImage(Project.FileName);
                await _uploadService.UpdateProjectImage(file, Project.Id);
            }

            return Redirect("/Admin/ProjectLair");
        }

    }
}
