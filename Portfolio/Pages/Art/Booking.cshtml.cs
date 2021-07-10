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

        public BookingModel(IAdmin admin, IEmail email)
        {
            _admin = admin;
            _email = email;
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
            RequestForm message = new RequestForm()
            {
                Name = RequestForm.Name,
                Email = RequestForm.Email,
                Body = RequestForm.Body,
                Availability = RequestForm.Availability
            };
            EmailResponse response = await _email.SendEmailAsync(message);
            HomePage = await _admin.GetHomePage("Booking");

            if (response.WasSent) WasSent = true;

            Redirect("/Art/Booking");
        }
    }
}
