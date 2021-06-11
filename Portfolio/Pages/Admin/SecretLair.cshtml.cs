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

        public List<HomePage> HomePages { get; set; } 

        [BindProperty]
        public HomePage HomePage { get; set; }

        public async Task OnGet()
        {
            try
            {
                HomePages = await _adminContext.GetHomePages();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }
        }

        /// <summary>
        /// Update selfie photo on respective home page
        /// </summary>
        /// <param name="file"> new image </param>
        /// <param name="homepage"> homepage object </param>
        public async Task<IActionResult> OnPostUpdateSelfie(IFormFile file, HomePage homepage)
        {
            if (file != null)
            {
                if (homepage.FileName != null)
                {
                    await _adminContext.DeleteBlobImage(homepage.FileName);
                }
                await _uploadService.UpdateSelfie(file, homepage.Id);
            }
            return Redirect("/Admin/SecretLair");
        }

        /// <summary>
        /// Edit homepage object data and save to database
        /// </summary>
        /// <param name="homepage"> homepage object </param>
        public async Task<IActionResult> OnPostEdit(HomePage homepage)
        {
            if (homepage.Intro == null)
                homepage.Intro = (await _adminContext.GetHomePage(homepage.Page)).Intro;

            HomePage updatedPage = new HomePage()
            {
                Id = homepage.Id,
                Page = homepage.Page,
                Selfie = homepage.Selfie,
                FileName = homepage.FileName,
                Title = homepage.Title,
                Intro = homepage.Intro
            };
            await _adminContext.UpdateHomePage(updatedPage);
            return Redirect("/Admin/SecretLair");
        }

        /// <summary>
        /// Updates database with most recent instagram media.
        /// </summary>
        public async Task<IActionResult> OnPostUpdateInstaFeed()
        {
            await _adminContext.GetInstagramFeed();
            return Redirect("/Admin/SecretLair");
        }

        /// <summary>
        /// Refresh instagram access token.
        /// </summary>
        public async Task<IActionResult> OnGetRefreshInstaToken()
        {
            await _adminContext.RefreshAccessToken();
            return Redirect("/Admin/SecretLair");
        }

    }
}
