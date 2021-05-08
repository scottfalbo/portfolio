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

        public List<Tattoo> TattooList { get; set; }

        [BindProperty]
        public Tattoo Tattoo { get; set; }

        [BindProperty]
        public string ImageUri { get; set; }
        public async Task OnGet()
        {
            try
            {
               TattooList = await _adminContext.GetTattoos();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }
        }

        public async Task<IActionResult> OnPostAddTattoo(IFormFile file)
        {
            if (file != null)
                await _uploadService.AddTattooImage(file);
            return Redirect("/Admin/TattooLair");
        }

        public async Task<IActionResult> OnPostEdit(Tattoo tattoo)
        {

            Tattoo updatedTattoo = new Tattoo()
            {
                Id = tattoo.Id,
                ImageURL = tattoo.ImageURL,
                FileName = tattoo.FileName,
                Order = tattoo.Order,
                Display = tattoo.Display
            };

            await _adminContext.UpdateTattoo(updatedTattoo);
            return Redirect("/Admin/ProjectLair");
        }

        public async Task<IActionResult> OnPostUpdateImage(IFormFile file)
        {
            if (file != null)
            {
                await _adminContext.DeleteBlobImage(Tattoo.FileName);
                await _uploadService.UpdateTattooImage(file, Tattoo.Id);
            }

            return Redirect("/Admin/TattooLair");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _adminContext.DeleteTattoo(Tattoo.Id);
            return Redirect("/Admin/TattooLair");
        }
    }
}
