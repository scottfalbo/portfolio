using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Portfolio.Email.Models;
using Portfolio.Email.Models.Interface;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Code
{
    public class ScottFalboCodeModel : PageModel
    {
        public IAdmin _adminContext;
        public IEmail _email;
        public IConfiguration _config;
        public string CaptchaKey;

        public ScottFalboCodeModel(IAdmin context, IEmail email, IConfiguration config)
        {
            _adminContext = context;
            _email = email;
            _config = config;
            CaptchaKey = _config["CaptchaKey"];
        }

        public List<Project> ProjectList { get; set; }
        public HomePage HomePage { get; set; }

        [BindProperty]
        public GeneralContact Contact { get; set; }

        [BindProperty]
        public bool WasSent { get; set; }

        public async Task OnGet()
        {
            try
            {
                ProjectList = await _adminContext.GetProjects();
                ProjectList.Reverse();
                HomePage = await _adminContext.GetHomePage("Code");
                Contact = new GeneralContact();
            }
            catch (Exception e)
            {
                RedirectToPage("Opps", new { error = e });
            }

        }

        /// <summary>
        /// General contact form, calls SendGrid method with form input
        /// </summary>
        public async Task OnPostSend()
        {
            RequestForm message = new RequestForm()
            {
                Name = Contact.Name,
                Email = Contact.Email,
                Body = Contact.Body
            };
            EmailResponse response = await _email.SendEmailAsync(message);

            if (response.WasSent) WasSent = true;

            HomePage = await _adminContext.GetHomePage("Code");
            ProjectList = await _adminContext.GetProjects();

            Redirect("/");
        }
    }
}
