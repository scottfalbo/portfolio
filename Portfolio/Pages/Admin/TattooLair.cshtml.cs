using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Admin
{
    public class TattooLairModel : PageModel
    {
        public IArtAdmin _adminContext;
        public IUploadService _uploadService;

        public TattooLairModel(IArtAdmin context, IUploadService upload)
        {
            _adminContext = context;
            _uploadService = upload;
        }

        public List<Image> TattooList { get; set; }

        [BindProperty]
        public Image Tattoo { get; set; }

        /// <summary>
        /// Gets a list of all Tattoo objects in the database
        /// </summary>
        public async Task OnGet()
        {
            try
            {
               TattooList = await _adminContext.GetImages();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }
        }

        /// <summary>
        /// Upload a new image to azure storage
        /// Instatiate a new tattoo object and save to database
        /// </summary>
        /// <param name="file"> input file </param>
        /// <returns> redirects in place </returns>
        public async Task<IActionResult> OnPostAddTattoo(IFormFile[] files)
        {
            foreach (var file in files)
            {
                if (file != null)
                    await _uploadService.AddTattooImage(file);
            }
            return Redirect("/Admin/TattooLair");
        }

        /// <summary>
        /// Update tattoo Order and Display data and save to database
        /// </summary>
        /// <param name="tattoo"> Tattoo object </param>
        /// <returns> redirect in place </returns>
        public async Task<IActionResult> OnPostEdit(Image tattoo)
        {
            Image updatedTattoo = new Image()
            {
                Id = tattoo.Id,
                Title = tattoo.Title,
                ImageURL = tattoo.ImageURL,
                FileName = tattoo.FileName,
                Order = tattoo.Order
            };

            await _adminContext.UpdateImage(updatedTattoo);
            return Redirect("/Admin/TattooLair");
        }

        /// <summary>
        /// Upload a new image and save the new path to object in database
        /// Delete the old image from azure storage
        /// </summary>
        /// <param name="file"> input file </param>
        /// <returns> redirect in place </returns>
        public async Task<IActionResult> OnPostUpdateImage(IFormFile file)
        {
            if (file != null)
            {
                await _adminContext.DeleteBlobImage(Tattoo.FileName);
                await _uploadService.UpdateTattooImage(file, Tattoo.Id);
            }

            return Redirect("/Admin/TattooLair");
        }

        /// <summary>
        /// Delete a tattoo object from the database
        /// Remove the associated image from azure storage
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDelete()
        {
            await _adminContext.DeleteImage(Tattoo.Id);
            return Redirect("/Admin/TattooLair");
        }

    }
}
