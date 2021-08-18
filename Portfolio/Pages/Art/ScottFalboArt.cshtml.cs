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

        [BindProperty]
        public GalleryToggle GalleryToggle { get; set; }

        public async Task OnGet()
        {
            Galleries = await _art.GetGalleries();
            HomePage = await _admin.GetHomePage("Tattoo");
            GalleryToggle = new GalleryToggle()
            {
                ActiveGalleryId = 0,
                StayCollapsed = true
            };
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

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task OnPostDeleteImage()
        {
            GalleryToggle = new GalleryToggle()
            {
                ActiveGalleryAdmin = true
            };
            
            //remove from gallery, GalleryImage
            //remove from database and blob

            Galleries = await _art.GetGalleries();
            HomePage = await _admin.GetHomePage("Tattoo");

            Redirect("/Art/ScottFalboArt");
        }

        /// <summary>
        /// Instantiates and saves a new gallery to the database.
        /// CreateGallery method auto assigns order to last and creates class names for bootstrap accordion.
        /// </summary>
        /// <param name="title"> new gallery title from input form </param>
        public async Task OnPostNewGallery(string title)
        {
            await _art.CreateGallery(title);

            GalleryToggle = new GalleryToggle()
            {
                ActiveGalleryAdmin = true,
                StayCollapsed = true
            };

            Galleries = await _art.GetGalleries();
            HomePage = await _admin.GetHomePage("Tattoo");

            Redirect("/Art/ScottFalboArt");
        }

        /// <summary>
        /// Remove a Gallery and all associated Image and GalleryImage records from database.
        /// </summary>
        /// <param name="id"> gallery id </param>
        public async Task OnPostDeleteGallery(int id)
        {
            await _art.DeleteGallery(id);

            GalleryToggle = new GalleryToggle()
            {
                ActiveGalleryAdmin = true,
                StayCollapsed = true
            };

            Galleries = await _art.GetGalleries();
            HomePage = await _admin.GetHomePage("Tattoo");

            Redirect("/Art/ScottFalboArt");
        }

        /// <summary>
        /// Update galleries display preference in database.
        /// </summary>
        public async Task OnPostUpdateGallery(string title)
        {
            Gallery gallery = await _art.GetGallery(GalleryToggle.GalleryId);
            gallery.Display = GalleryToggle.Display;
            gallery.Title = title;

            await _art.UpdateGallery(gallery);

            GalleryToggle = new GalleryToggle()
            {
                ActiveGalleryAdmin = true
            };

            Galleries = await _art.GetGalleries();
            HomePage = await _admin.GetHomePage("Tattoo");

            Redirect("/Art/ScottFalboArt");
        }
    }

    /// <summary>
    /// Gallery toggle properties
    /// </summary>
    public class GalleryToggle
    {
        // gallery id from form
        public int GalleryId { get; set; }
        // bool from form used to toggle gallery display
        public bool Display { get; set; }
        // image id from form
        public int ImageId { get; set; }
        // bool that is toggled to keep admin window open on refresh
        public bool ActiveGalleryAdmin { get; set; }
        // gallery id of active gallery when images are edited to reopen in place on refresh
        public int ActiveGalleryId { get; set; }
        // bool to decide whether or not to keep the galleries collapsed on reload
        public bool StayCollapsed { get; set; }
    }
}
