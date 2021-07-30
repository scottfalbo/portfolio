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
    public class DrawingLairModel : PageModel
    {
        public IArtAdmin _adminContext;
        public IUploadService _uploadService;

        public DrawingLairModel(IArtAdmin context, IUploadService upload)
        {
            _adminContext = context;
            _uploadService = upload;
        }

        public List<Image> DrawingList { get; set; }

        [BindProperty]
        public Image Drawing { get; set; }

        /// <summary>
        /// Gets a list of all Drawing objects in the database
        /// </summary>
        public async Task OnGet()
        {
            try
            {
                DrawingList = await _adminContext.GetImages();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }
        }

        /// <summary>
        /// Upload a new image to azure storage
        /// Instatiate a new drawing object and save to database
        /// </summary>
        /// <param name="file"> input file </param>
        /// <returns> redirects in place </returns>
        public async Task<IActionResult> OnPostAddDrawing(IFormFile[] files)
        {
            foreach (var file in files)
            {
                if (file != null)
                    await _uploadService.AddDrawingImage(file);
            }
            return Redirect("/Admin/DrawingLair");
        }

        /// <summary>
        /// Update drawing Order and Display data and save to database
        /// </summary>
        /// <param name="drawing"> Drawing object </param>
        /// <returns> redirect in place </returns>
        public async Task<IActionResult> OnPostEdit(Image image)
        {

            Image updatedDrawing = new Image()
            {
                Id = image.Id,
                Title = image.Title,
                Type = image.Type,
                ImageURL = image.ImageURL,
                FileName = image.FileName,
                Order = image.Order
            };

            await _adminContext.UpdateImage(updatedDrawing);
            return Redirect("/Admin/DrawingLair");
        }

        /// <summary>
        /// Delete a tattoo object from the database
        /// Remove the associated image from azure storage
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDelete()
        {
            await _adminContext.DeleteImage(Drawing.Id);
            return Redirect("/Admin/DrawingLair");
        }
    }
}
