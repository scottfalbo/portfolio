using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Admin
{
    public class ProjectLairModel : PageModel
    {
        public IAdmin _adminContext;
        public IArtAdmin _artAdminContext;

        public ProjectLairModel(IAdmin context, IArtAdmin artAdmin)
        {
            _adminContext = context;
            _artAdminContext = artAdmin;
        }

        public List<Project> ProjectList { get; set; }

        [BindProperty]
        public Project Project { get; set; }

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
            if (project.Description == null)
                Project.Description = (await _adminContext.GetProject(3)).Description;

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

        public async Task<IActionResult> OnPostDelete()
        {
            await _adminContext.DeleteProject(Project.Id);
            return Redirect("/Admin/ProjectLair");
        }

    }
}
