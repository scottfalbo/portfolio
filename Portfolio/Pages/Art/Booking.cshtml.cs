using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Portfolio.Email.Models;
using Portfolio.Email.Models.Interface;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Art
{
    public class BookingModel : PageModel
    {
        public IAdmin _admin;
        public IEmail _email;
        public IUploadService _upload;
        public IConfiguration _config;
        public string CaptchaKey;

        public BookingModel(IAdmin admin, IEmail email, IUploadService upload, IConfiguration config)
        {
            _admin = admin;
            _email = email;
            _upload = upload;
            _config = config;
            CaptchaKey = _config["CaptchaKey"];
        }

        public HomePage HomePage { get; set; }

        [BindProperty]
        public RequestForm RequestForm { get; set; }

        [BindProperty]
        public bool WasSent { get; set; }

        public async Task OnGet()
        {
            HomePage = await _admin.GetHomePage("Booking");
            RequestForm = new RequestForm();
        }

        public async Task<IActionResult> OnPostEdit(HomePage homepage)
        {
            if (homepage.Intro == null)
                homepage.Intro = (await _admin.GetHomePage(homepage.Page)).Intro;

            HomePage updatedPage = new HomePage()
            {
                Id = homepage.Id,
                Page = homepage.Page,
                Title = homepage.Title,
                Intro = homepage.Intro
            };
            await _admin.UpdateHomePage(updatedPage);
            return Redirect("/Art/Booking");
        }

        /// <summary>
        /// Build Message object with user input and call SendGrid method.
        /// </summary>
        /// <returns> Redirect </returns>
        public async Task OnPostSend(IFormFile[] files)
        {
            Uris images = new Uris();

            foreach (var file in files)
            {
                if (file != null)
                    images.ImageUris.Add(new ImageUri(((await _upload.UploadImage(file)).Uri).ToString()));
            }
            RequestForm message = new RequestForm()
            {
                Name = RequestForm.Name,
                Email = RequestForm.Email,
                Body = RequestForm.Body,
                Availability = RequestForm.Availability,
                Uris = images
            };
            EmailResponse response = await _email.SendEmailAsync(message);
            HomePage = await _admin.GetHomePage("Booking");

            if (response.WasSent) WasSent = true;

            Redirect("/Art/Booking");
        }
    }

    public class ImageUri 
    {
        public string Uri { get; set; }

        public ImageUri (string uri)
        {
            Uri = uri;
        }
    }
    public class Uris
    {
        public List<ImageUri> ImageUris { get; set; }

        public Uris()
        {
            ImageUris = new List<ImageUri>();
        }
    }
}
