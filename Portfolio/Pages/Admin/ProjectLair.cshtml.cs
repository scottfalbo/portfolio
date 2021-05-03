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


    }
}
