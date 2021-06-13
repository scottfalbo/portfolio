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
    public class StudioArcanumModel : PageModel
    {
        public IAdmin _admin;

        public StudioArcanumModel(IAdmin admin)
        {
            _admin = admin;
        }

        public HomePage HomePage { get; set; } 

        public async void OnGet()
        {
            HomePage = await _admin.GetHomePage("Studio");
        }
    }
}
