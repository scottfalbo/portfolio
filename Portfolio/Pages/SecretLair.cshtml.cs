using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models.Interfaces;

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
        public void OnGet()
        {

        }
    }
}
