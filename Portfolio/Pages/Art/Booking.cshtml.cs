using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public BookingModel(IAdmin admin, IEmail email, IUploadService upload)
        {
            _admin = admin;
            _email = email;
            _upload = upload;
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
                    images.ImageUris.Add( new ImageUri((await _upload.UploadImage(file)).Uri));
            }
            RequestForm message = new RequestForm()
            {
                Name = RequestForm.Name,
                Email = RequestForm.Email,
                Body = RequestForm.Body,
                Availability = RequestForm.Availability,
                Images = images
            };
            Console.WriteLine("");
            EmailResponse response = await _email.SendEmailAsync(message);
            HomePage = await _admin.GetHomePage("Booking");

            if (response.WasSent) WasSent = true;

            Redirect("/Art/Booking");
        }
    }

    public class ImageUri 
    {
        public Uri Uri { get; set; }

        public ImageUri (Uri uri)
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
