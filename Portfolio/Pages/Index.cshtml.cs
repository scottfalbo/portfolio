using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Portfolio.Models;
using Portfolio.Models.Interface;
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

        public HomePage HomePage { get; set; }

        public async Task OnGet()
        {
            try
            {
                HomePage = await _adminContext.GetHomePage("Home");
                await _adminContext.GetInstagramFeed();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new {error = e});
            }

        }
    }
}
