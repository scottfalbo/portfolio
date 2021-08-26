using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Portfolio.Email.Models;
using Portfolio.Email.Models.Interface;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Code
{
    public class ScottFalboCodeModel : PageModel
    {
        public IAdmin _admin;

        public ScottFalboCodeModel(IAdmin admin)
        {
            _admin = admin;
        }

        public List<Project> Projects { get; set; }
        public HomePage HomePage { get; set; }


        public async Task OnGet()
        {
            try
            {
                Projects = await _admin.GetProjects();
                Projects.Reverse();
                HomePage = await _admin.GetHomePage("Code");
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }

        }

    }
}
