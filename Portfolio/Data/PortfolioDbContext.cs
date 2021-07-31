using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portfolio.Auth.Models;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public class PortfolioDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<HomePage> HomePage { get; set; }
        public DbSet<Instagram> Instragrams { get; set; }
        public DbSet<Studio> Studio { get; set; }

        public IConfiguration Configuration { get; }

        public PortfolioDbContext(DbContextOptions options) : base(options){}

        public PortfolioDbContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            Configuration = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //SeedRole(modelBuilder, "admin", "create", "read", "update", "delete");

            string id = "a18be8c0-aa67-4af8-bd17-00bd6346e575";

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = id,
                Name = "admin",
                NormalizedName = "admin"
            });

            var admin = Configuration["Name"];
            var adminPass = Configuration["Pass"];

            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = id,
                UserName = admin,
                NormalizedUserName = admin,
                Email = "scottfalboart@gmail.com",
                NormalizedEmail = "scottfalboart@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, adminPass),
                SecurityStamp = string.Empty
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = id,
                UserId = id
            });

            modelBuilder.Entity<HomePage>().HasData(
                new HomePage
                {
                    Id = 1,
                    Page = "Home",
                    Title = "Software Developer | Artist",
                    Intro = "Software developer and artist in Seattle.",
                    Selfie = "",
                    FileName = "code-selfie.png"
                });
            
            modelBuilder.Entity<HomePage>().HasData(
                new HomePage
                {
                    Id = 2,
                    Page = "Tattoo",
                    Title = "Tattoo Artist | Studio Arcanum",
                    Intro = "I do tattoos some times",
                    Selfie = "",
                    FileName = "code-selfie.png"
                });
            
            modelBuilder.Entity<HomePage>().HasData(
                new HomePage
                {
                    Id = 3,
                    Page = "Code",
                    Title = "Software Developer | C# .NET",
                    Intro = "I write code better than bios, coming soon...",
                    Selfie = "",
                    FileName = "code-selfie.png"
                });
          
            modelBuilder.Entity<HomePage>().HasData(
                new HomePage
                {
                    Id = 4,
                    Page = "Booking",
                    Title = "",
                    Intro = "Booking information",
                    Selfie = "",
                    FileName = ""
                });          
            
            modelBuilder.Entity<Studio>().HasData(
                new Studio
                {
                    Id = 1,
                    Intro ="Studio Arcanum \nSeattle Wa",
                    Policies = "...",
                    Aftercare = "..."
                });

        }

        private int id = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaims = permissions.Select(permission =>
               new IdentityRoleClaim<string>
               {
                   Id = id++,
                   RoleId = role.Id,
                   ClaimType = "permissions",
                   ClaimValue = permission
               });
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }
}
