using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages
{
    [Authorize]
    public class SecretLairModel : PageModel
    {

        public IAdmin _adminContext;

        public SecretLairModel(IAdmin context)
        {
            _adminContext = context;
        }

        [BindProperty]
        public HomePage HomePage { get; set; }

        public async Task OnGet()
        {
            try
            {
                HomePage = await _adminContext.GetHomePage();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }
        }



    }
}
