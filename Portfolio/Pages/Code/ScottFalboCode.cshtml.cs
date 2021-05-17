using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Code
{
    public class ScottFalboCodeModel : PageModel
    {
        public IAdmin _adminContext;

        public ScottFalboCodeModel(IAdmin context)
        {
            _adminContext = context;
        }

        public List<Project> ProjectList { get; set; }
        public HomePage HomePage { get; set; }

        public async Task OnGet()
        {
            try
            {
                ProjectList = await _adminContext.GetProjects();
                HomePage = await _adminContext.GetHomePage("Code");
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }

        }
    }
}
