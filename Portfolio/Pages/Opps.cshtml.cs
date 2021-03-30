using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages
{
    public class OppsModel : PageModel
    {
        [BindProperty(Name = "error", SupportsGet = true)]
        public Exception E { get; set; }
        public void OnGet()
        {

        }
    }
}
