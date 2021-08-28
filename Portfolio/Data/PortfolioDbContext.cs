using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Portfolio.Auth.Models;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryImage> GalleryImage { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
        public DbSet<HomePageTechnology> HomePageTechnologies { get; set; }

        public IConfiguration Configuration { get; }

        public PortfolioDbContext(DbContextOptions options) : base(options){}

        public PortfolioDbContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            Configuration = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GalleryImage>().HasKey(x => new { x.GalleryId, x.ImageId });
            modelBuilder.Entity<ProjectTechnology>().HasKey(x => new { x.ProjectId, x.TechnologyId });
            modelBuilder.Entity<ProjectImage>().HasKey(x => new { x.ProjectId, x.ImageId });
            modelBuilder.Entity<HomePageTechnology>().HasKey(x => new { x.HomePageId, x.TechnologyId });

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

            /// Page information seeds
            modelBuilder.Entity<HomePage>().HasData(
                new HomePage
                {
                    Id = 1,
                    Page = "Home",
                    Title = "Software Developer | Artist",
                    Intro = "Software developer and artist in Seattle.",
                    Selfie = "",
                    FileName = "code-selfie.png"
                },
                new HomePage
                {
                    Id = 2,
                    Page = "Tattoo",
                    Title = "Tattoo Artist | Studio Arcanum",
                    Intro = "I do tattoos some times",
                    Selfie = "",
                    FileName = "code-selfie.png"
                },
                new HomePage
                {
                    Id = 3,
                    Page = "Code",
                    Title = "Software Developer | C# .NET",
                    Intro = "I write code better than bios, coming soon...",
                    Selfie = "",
                    FileName = "code-selfie.png"
                },
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
                    Policies = "....",
                    Aftercare = "...."
                });

            /// Gallery seeds
            modelBuilder.Entity<Gallery>().HasData(
                new Gallery
                { 
                    Id = 1,
                    Title = "Gallery One",
                    AccordionId = "galleryone",
                    Display = true,
                    Order = 1,
                    CollapseId = "galleryone1",
                    AdminAccordionId = "galleryoneadmin",
                    AdminCollapseId = "galleryone1admin"
                },
                new Gallery
                {
                    Id = 2,
                    Title = "Gallery Two",
                    AccordionId = "gallerytwo",
                    Display = true,
                    Order = 2,
                    CollapseId = "gallerytwo2",
                    AdminAccordionId = "gallerytwoadmin",
                    AdminCollapseId = "gallerytwo2admin"
                },
                new Gallery
                {
                    Id = 3,
                    Title = "Gallery Three",
                    AccordionId = "gallerythree",
                    Display = true,
                    Order = 3,
                    CollapseId = "gallerythree3",
                    AdminAccordionId = "gallerythreeadmin",
                    AdminCollapseId = "gallerythree3admin"
                }
            );

            /// Image seeds
            for (int i = 1; i < 37; i++)
            {
                modelBuilder.Entity<Image>().HasData(
                    new Image
                    {
                        Id = i,
                        Title = $"testimage{i}",
                        ImageURL = "https://via.placeholder.com/700",
                        ThumbURL = "https://via.placeholder.com/80",
                        ThumbFileName = "blank_thumb",
                        FileName = "blank",
                        Order = i
                    });
            }

            for (int i = 1; i < 13; i++)
            {
                modelBuilder.Entity<GalleryImage>().HasData(
                    new GalleryImage
                    {
                        GalleryId = 1,
                        ImageId = i
                    },
                    new GalleryImage
                    {
                        GalleryId = 2,
                        ImageId = i+12
                    },
                    new GalleryImage
                    {
                        GalleryId = 3,
                        ImageId = i+24
                    }
                );
            }

            string path = @"Data/technologiesData.json";
            TechnologiesList techList = JsonConvert.DeserializeObject<TechnologiesList>(File.ReadAllText(path));

            foreach(Technology tech in techList.technologies)
            {
                modelBuilder.Entity<Technology>().HasData(
                    new Technology
                    {
                        Id = tech.Id,
                        Title = tech.Title,
                        Type = tech.Type,
                        LogoUrl = tech.LogoUrl,
                    }
                );
            }

            modelBuilder.Entity<Project>().HasData(
                    new Project
                    {
                        Id = 1,
                        Title = "Project One",
                        AltText = "project image",
                        Description = "It does some things",
                        TechSummary = "I used these things",
                        RepoLink = "https://github.com/scottfalbo",
                        DeployedLink = "https://scottfalbo.com",
                        Order = 1,
                        Display = true,
                        AccordionId = "projectone",
                        CollapseId = "projectone1",
                        AdminAccordionId = "projectoneadmin",
                        AdminCollapseId = "projectone1admin"
                    },
                    new Project 
                    {
                        Id = 2,
                        Title = "Project Two",
                        AltText = "project image",
                        Description = "It does some things",
                        TechSummary = "I used these things",
                        RepoLink = "https://github.com/scottfalbo",
                        DeployedLink = "https://scottfalbo.com",
                        Order = 2,
                        Display = true,
                        AccordionId = "projecttwo",
                        CollapseId = "projecttwo2",
                        AdminAccordionId = "projecttwoadmin",
                        AdminCollapseId = "projecttwo2admin"
                    }
                );

            foreach (Technology tech in techList.technologies)
            {
                modelBuilder.Entity<ProjectTechnology>().HasData(
                    new ProjectTechnology
                    {
                        ProjectId = 1,
                        TechnologyId = tech.Id
                    },
                    new ProjectTechnology
                    {
                        ProjectId = 2,
                        TechnologyId = tech.Id
                    }
                );
                modelBuilder.Entity<HomePageTechnology>().HasData(
                    new HomePageTechnology
                    { 
                        HomePageId = 3,
                        TechnologyId = tech.Id
                    }
                );
            }

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

    public class TechnologiesList
    {
        public List<Technology> technologies { get; set; }
    }
}
