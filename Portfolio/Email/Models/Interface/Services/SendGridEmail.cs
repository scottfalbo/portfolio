using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Email.Models.Interface.Services
{
    public class SendGridEmail : IEmail
    {
        public IConfiguration Configuration { get; set; }

        public SendGridEmail(IConfiguration config)
        {
            Configuration = config;
        }

        /// <summary>
        /// Email the request via Send Grid
        /// </summary>
        /// <param name="inboundData"> Message object with form input </param>
        /// <returns> EmailResponse object with success bool </returns>
        public async Task<EmailResponse> SendEmailAsync(RequestForm inboundData)
        {
            string key = Configuration["Key"];
            string email = Configuration["FromEmail"];
            string name = Configuration["FromName"];

            var client = new SendGridClient(key);

            SendGridMessage message = new SendGridMessage();
            message.SetFrom(new EmailAddress(email, name));
            message.AddTo("scottfalboart@gmail.com");
            message.SetTemplateId("d-a040e40c67a44f9fa18aa2762c9aadfc");
            message.SetTemplateData(new SendGridTemplate
            {
                name = inboundData.Name,
                email = inboundData.Email,
                body = inboundData.Body,
                availability = inboundData.Availability
            });


            var response = await client.SendEmailAsync(message);
            return new EmailResponse()
            {
                WasSent = response.IsSuccessStatusCode
            };
        }
    }

    /// <summary>
    /// Class model for SendGrid template object
    /// </summary>
    public class SendGridTemplate
    {
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
        public string availability { get; set; }
    }
}
