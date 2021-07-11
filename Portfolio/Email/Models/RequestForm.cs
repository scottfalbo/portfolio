using Portfolio.Pages.Art;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Email.Models
{
    public class RequestForm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public string Availability { get; set; }
        public Uris Uris { get; set; }
    }
}
