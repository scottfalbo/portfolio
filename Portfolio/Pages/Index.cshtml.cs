﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
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
        public IConfiguration _config;
        public string CaptchaKey;

        public IndexModel (IAdmin context, IEmail email, IConfiguration config)
        {
            _adminContext = context;
            _email = email;
            _config = config;
            CaptchaKey = _config["CaptchaKey"];
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

        /// <summary>
        /// General contact form that calls SendGrid with input data
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

            HomePage = await _adminContext.GetHomePage("Home");
            Redirect("/");
        }
    }
}
