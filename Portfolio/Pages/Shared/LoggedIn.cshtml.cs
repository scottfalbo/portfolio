using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages.Shared
{
    public class LoggedInModel : PageModel
    {
        public JsonResult OnGet()
        {
            return new JsonResult(User.Identity.IsAuthenticated ? "true" : "false");
        }
    }
}
