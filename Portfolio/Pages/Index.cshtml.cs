using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Portfolio.Email.Models;
using Portfolio.Email.Models.Interface;
using Portfolio.Models;
using Portfolio.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages
{
    public class IndexModel : PageModel
    {
        public IAdmin _adminContext;
        public IEmail _email;

        public IndexModel (IAdmin context, IEmail email)
        {
            _adminContext = context;
            _email = email;
        }

        public HomePage HomePage { get; set; }

        [BindProperty]
        public GeneralContact Contact { get; set; }

        [BindProperty]
        public bool WasSent { get; set; }

        public async Task OnGet()
        {
            try
            {
                HomePage = await _adminContext.GetHomePage("Home");
                Contact = new GeneralContact();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new {error = e});
            }
        }

        public async Task OnPostSend()
        {
            Message message = new Message()
            {
                To = "scottfalboart@gmail.com",
                Subject = $"General Contact Form from {Contact.Name}",
                Body = $"{Contact.Body} \n {Contact.Email}"
            };
            EmailResponse response = await _email.SendEmailAsync(message);

            if (response.WasSent) WasSent = true;

            HomePage = await _adminContext.GetHomePage("Home");
            Redirect("/");
        }
    }
}
