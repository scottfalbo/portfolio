using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Auth.Models;
using Portfolio.Auth.Models.Interfaces;

namespace Portfolio.Pages
{
    public class SecretEntranceModel : PageModel
    {
        public IUserService service { get; }

        public SecretEntranceModel(IUserService userService)
        {
            service = userService;
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string userName, string password)
        {
            LoginData newUser = new LoginData()
            {
                UserName = userName,
                Password = password
            };

            var user = await service.Authenticate(newUser.UserName, newUser.Password);

            if (user != null)
                return Redirect("Admin/SecretLair");
            return Redirect("Opps");
        }
    }
}
