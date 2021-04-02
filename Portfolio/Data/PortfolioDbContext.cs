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
        public DbSet<Tattoo> Tattoos { get; set; }
        public IConfiguration Configuration { get; }

        public PortfolioDbContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            Configuration = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedRole(modelBuilder, "admin", "create", "read", "update", "delete");

            string id = "a18be8c0-aa67-4af8-bd17-00bd6346e575";

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = id,
                Name = "admin",
                NormalizedName = "admin"
            });

            var admin = Configuration["Portfolio:AdminName"]; 
            var adminPass = Configuration["Portfolio:AdminPass"];

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


            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = -1,
                    Title = "SmallBoi, The Game",
                    SourceURL = "images/smallboi.png",
                    Description = "SmallBoi is a two player coop platform puzzle game built in Unity.  It has both local and network multiple player options using Photon.",
                    RepoLink = "https://github.com/AmeiliaAndTheSmallBois/SmallBoi/tree/main",
                    AltText = "SmallBoi screenshot",
                    Order = 2
                },
                new Project
                {
                    Id = 2,
                    Title = "LiteBerry Pi",
                    SourceURL = "images/liteberrypi.png",
                    Description = "LiteBerry Pi allows users to create and send designs to a RaspBerry Pi with a matrix of led lights attached.  The app uses an api to create and save designs.  The api also contains a route to send designs to the Pi using a SignalR server.",
                    AltText = "LiteBerryPi screenshot",
                    RepoLink = "https://github.com/Lite-Berry-pi/Lite-Berry-Pi",
                    Order = 1
                },
                new Project
                {
                    Id = 3,
                    Title = "React Minesweeper",
                    SourceURL = "images/minesweeper.png",
                    Description = "A re-creation of the Window's classic Minesweeper.  I built this to practice components and state within a React App.",
                    AltText = "Minesweeper App screenshot",
                    RepoLink = "https://github.com/scottfalbo/react-minesweeper-v2",
                    DeployedLink = "https://scottfalbo.github.io/react-minesweeper-v2/",
                    Order = 3
                }
            );
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
