using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Portfolio.Models;
using Portfolio.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages
{
    public class IndexModel : PageModel
    {
        public IAdmin _adminContext;

        public IndexModel (IAdmin context)
        {
            _adminContext = context;
        }

        public List<Project> ProjectList { get; set; }

        public async Task OnGet()
        {
            try
            {
                ProjectList = await _adminContext.GetProjects();
                Console.WriteLine("whatever");
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new {error = e});
            }

        }
    }
}
