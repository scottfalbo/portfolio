using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Art
{
    public class TattoosModel : PageModel
    {
        public IArtAdmin _adminContext;

        public TattoosModel(IArtAdmin admin)
        {
            _adminContext = admin;
        }

        [BindProperty]
        public List<Tattoo> Tattoos { get; set; }

        public async Task OnGet()
        {
            Tattoos = await _adminContext.GetTattoos();
        }
    }
}
