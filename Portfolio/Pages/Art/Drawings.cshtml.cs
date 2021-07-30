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
    public class DrawingsModel : PageModel
    {
        public IArtAdmin _artAdmin;

        public DrawingsModel(IArtAdmin art)
        {
            _artAdmin = art;
        }

        [BindProperty]
        public List<Image> Drawings { get; set; }

        public async Task OnGet()
        {
            Drawings = await _artAdmin.GetImages();
            Drawings.Reverse();
        }
    }
}
