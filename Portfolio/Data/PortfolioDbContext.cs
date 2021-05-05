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
        public DbSet<Drawing> Drawings { get; set; }
        public DbSet<Design> Designs { get; set; }

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
                    Order = 0
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
