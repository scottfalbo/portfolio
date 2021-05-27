using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Email.Models.Interface
{
    public interface IEmail
    {
        public Task<EmailResponse> SendEmailAsync(Message inboundData);
    }
}
