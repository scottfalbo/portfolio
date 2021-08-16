using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public IUploadService _upload;

        public ScottFalboArtModel(IAdmin admin, IArtAdmin art, IUploadService upload)
        {
            _admin = admin;
            _art = art;
            _upload = upload;
        }

        public HomePage HomePage { get; set; }
        public List<Gallery> Galleries { get; set; }

        public async Task OnGet()
        {
            Galleries = await _art.GetGalleries();
            HomePage = await _admin.GetHomePage("Tattoo");
        }

        /// <summary>
        /// Update the pages main photo
        /// </summary>
        /// <param name="file"> new image </param>
        /// <param name="homepage"> homepage object </param>
        public async Task<IActionResult> OnPostUpdateSelfie(IFormFile file, HomePage homepage)
        {
            if (file != null)
            {
                if (homepage.FileName != null)
                {
                    await _admin.DeleteBlobImage(homepage.FileName);
                }
                await _upload.UpdateSelfie(file, homepage.Id);
            }
            return Redirect("/Art/ScottFalboArt");
        }

        /// <summary>
        /// Edit homepage object data and save to database
        /// </summary>
        /// <param name="homepage"> homepage object </param>
        public async Task<IActionResult> OnPostEdit(HomePage homepage)
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
            return Redirect("/Art/ScottFalboArt");
        }
    }
}
