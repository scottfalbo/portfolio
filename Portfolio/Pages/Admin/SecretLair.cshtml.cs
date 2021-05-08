using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages
{
    [Authorize]
    public class SecretLairModel : PageModel
    {

        public IAdmin _adminContext;
        public IUploadService _uploadService;

        public SecretLairModel(IAdmin context, IUploadService service)
        {
            _adminContext = context;
            _uploadService = service;
        }

        [BindProperty]
        public HomePage HomePage { get; set; }

        public async Task OnGet()
        {
            try
            {
                HomePage = await _adminContext.GetHomePage();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }
        }

        public async Task<IActionResult> OnPostUpdateSelfie(IFormFile file)
        {
            if (file != null)
            {
                if (HomePage.FileName != null)
                {
                    await _adminContext.DeleteBlobImage(HomePage.FileName);
                }
                await _uploadService.UpdateSelfie(file, -1);
            }
            return Redirect("/Admin/SecretLair");
        }

        public async Task OnPostEdit(HomePage homepage)
        {
            HomePage updatedPage = new HomePage()
            {
                Id = -1,
                Selfie = HomePage.Selfie,
                FileName = HomePage.FileName,
                Title = HomePage.Title,
                Intro = HomePage.Intro
            };
            await _adminContext.UpdateHomePage(updatedPage);
            Redirect("/Admin/SecretLair");
        }

    }
}
