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

        public ScottFalboArtModel(IAdmin admin)
        {
            _admin = admin;
        }

        public HomePage HomePage { get; set; }
        public List<Instagram> InstagramGallery { get; set; }

        public async Task OnGet()
        {
            HomePage = await _admin.GetHomePage("Tattoo");
            InstagramGallery = await _admin.GetInstagrams();
        }
    }
}
