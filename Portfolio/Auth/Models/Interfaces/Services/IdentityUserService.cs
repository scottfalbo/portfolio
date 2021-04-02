using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Portfolio.Auth.Models.Interfaces.Services
{
    public class IdentityUserService : IUserService
    {

        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly PortfolioDbContext _context;

        public IdentityUserService(UserManager<ApplicationUser> registerUser, SignInManager<ApplicationUser> sim, PortfolioDbContext context)
        {
            UserManager = registerUser;
            signInManager = sim;
            _context = context;
        }

        /// <summary>
        /// Authenticate a user at login time.
        /// </summary>
        /// <param name="userName"> user name </param>
        /// <param name="password"> user password </param>
        /// <returns> ApplicationUserDto with user info </returns>
        public async Task<ApplicationUserDto> Authenticate(string userName, string password)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password, true, false);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByNameAsync(userName);
                return new ApplicationUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await UserManager.GetRolesAsync(user),
                };
            }
            return null;
        }

        /// <summary>
        /// Register a new user and give them "guest" level persmission.
        /// </summary>
        /// <param name="data"> RegisterUser object </param>
        /// <param name="modelState"> ModelState </param>
        /// <returns> ApplicationUserDto object </returns>
        public async Task<ApplicationUserDto> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = data.UserName,
                Email = data.Email
            };
            var result = await UserManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                await UserManager.AddToRolesAsync(user, new List<string>() { "guest" });

                return new ApplicationUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = new List<string>() { "guest" },
                };
            }
            return new ApplicationUserDto();
        }
    }
}
