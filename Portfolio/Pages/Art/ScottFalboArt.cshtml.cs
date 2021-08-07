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
    public class ScottFalboArtModel : PageModel
    {
        public IAdmin _admin;
        public IArtAdmin _art;

        public ScottFalboArtModel(IAdmin admin, IArtAdmin art)
        {
            _admin = admin;
            _art = art;
        }

        public HomePage HomePage { get; set; }
        public List<Gallery> Galleries { get; set; }

        public async Task OnGet()
        {
            Galleries = await _art.GetGalleries();
            HomePage = await _admin.GetHomePage("Tattoo");
        }
    }
}
