using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Art
{
    public class BookingModel : PageModel
    {
        public IAdmin _admin;

        public BookingModel(IAdmin admin)
        {
            _admin = admin;
        }

        public HomePage HomePage { get; set; }

        [BindProperty]
        public RequestForm RequestForm { get; set; }

        public async Task OnGet()
        {
            HomePage = await _admin.GetHomePage("Booking");
            RequestForm = new RequestForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostSend()
        {
            
            return Redirect("/Art/Booking");
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
