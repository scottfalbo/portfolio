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
        public async Task<EmailResponse> SendEmailAsync(Message inboundData)
        {
            string key = Configuration["SendGrid:Key"];
            string email = Configuration["SendGrid:FromEmail"];
            string name = Configuration["SendGrid:FromName"];

            var client = new SendGridClient(key);

            SendGridMessage message = new SendGridMessage();
            message.SetFrom(new EmailAddress(email, name));
            message.AddTo(inboundData.To);
            message.SetSubject(inboundData.Subject);
            message.AddContent(MimeType.Html, inboundData.Body);

            var response = await client.SendEmailAsync(message);
            return new EmailResponse()
            {
                WasSent = response.IsSuccessStatusCode
            };
        }
    }
}
