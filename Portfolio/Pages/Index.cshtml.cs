using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Portfolio.Email.Models;
using Portfolio.Email.Models.Interface;
using Portfolio.Models;
using Portfolio.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages
{
    public class IndexModel : PageModel
    {
        public IAdmin _admin;
        public IUploadService _upload;
        public IConfiguration _config;

        public IndexModel (IAdmin admin, IUploadService upload, IConfiguration config)
        {
            _admin = admin;
            _upload = upload;
            _config = config;
        }

        public HomePage HomePage { get; set; }

        public async Task OnGet()
        {
            try
            {
                HomePage = await _admin.GetHomePage("Home");
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new {error = e});
            }
        }

        public async Task<IActionResult> OnPostUpdatePhoto(IFormFile file, HomePage homepage)
        {
            if (file != null)
            {
                if (homepage.FileName != null)
                {
                    await _admin.DeleteBlobImage(homepage.FileName);
                }
                await _upload.UpdateSelfie(file, homepage.Id);
            }
            HomePage = await _admin.GetHomePage("Home");
            return Redirect("/");
        }

        public async Task<IActionResult> OnPostUpdatePage(HomePage homepage)
        {
            if (homepage.Intro == null)
                homepage.Intro = (await _admin.GetHomePage(homepage.Page)).Intro;

            HomePage updatedPage = new HomePage()
            {
                Id = homepage.Id,
                Page = homepage.Page,
                Selfie = homepage.Selfie,
                FileName = homepage.FileName,
                Title = homepage.Title,
                Intro = homepage.Intro
            };
            await _admin.UpdateHomePage(updatedPage);
            HomePage = await _admin.GetHomePage("Home");
            return Redirect("/Index");
        }
    }
}
