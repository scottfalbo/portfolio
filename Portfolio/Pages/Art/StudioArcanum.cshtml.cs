using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Art
{
    public class StudioArcanumModel : PageModel
    {
        public IAdmin _admin;
        public IConfiguration Configuration;

        public StudioArcanumModel(IAdmin admin, IConfiguration config)
        {
            _admin = admin;
            Configuration = config;
        }
        
        [BindProperty]
        public Studio Studio { get; set; }

        public string GoogleMapRequest { get; set; }

        public async void OnGet()
        {
            Studio = await _admin.GetStudio(1);
            string googleApiKey = Configuration["GoogleMapKey"];
            GoogleMapRequest = $"https://maps.googleapis.com/maps/api/js?key={googleApiKey}&callback=initMap";
        }
    }
}
