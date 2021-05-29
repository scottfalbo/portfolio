using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task OnPostSend()
        {
            Message message = new Message()
            {
                To = "scottfalboart@gmail.com",
                Subject = $"Tattoo Request from {RequestForm.Name}",
                Body = $"{RequestForm.Idea} \n {RequestForm.Availability} \n {RequestForm.Email}"
            };
            EmailResponse response = await _email.SendEmailAsync(message);

            if (response.WasSent) WasSent = true;
            
            HomePage = await _admin.GetHomePage("Booking");
            await EmailClient(RequestForm.Email);

            Redirect("/Art/Booking");
        }

        /// <summary>
        /// Helper method to send a confirmation of to the client
        /// </summary>
        /// <param name="email"> email from form </param>
        private async Task EmailClient(string email)
        {
            Message message = new Message()
            {
                To = email,
                Subject = "Thanks for the request",
                Body = "some templated thank you"
            };
            await _email.SendEmailAsync(message);
        }
    }

    /// <summary>
    /// Email request object
    /// </summary>
    public class RequestForm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Idea { get; set; }
        public string Availability { get; set; }
    }
}
