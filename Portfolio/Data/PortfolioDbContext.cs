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

            string id = Configuration["AdminUserId"];

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
                    Intro = "Hello, and thanks for visiting my portfolio site!  For my software development portfolio please visit the Code section.  For my tattoo and drawing portfolios please visit the Art section.",
                    Selfie = "https://falboportfoliostorage.blob.core.windows.net/images/code-selfie932021115101.png",
                    FileName = "code-selfie932021115101.png"
                },
                new HomePage
                {
                    Id = 2,
                    Page = "Tattoo",
                    Title = "Tattoo Artist | Studio Arcanum",
                    Intro = "Hello, thanks for checking out my art page.  I make tattoos and art at Studio Arcanum in the Fremont neighborhood of Seattle.  ",
                    Selfie = "https://falboportfoliostorage.blob.core.windows.net/images/code-selfie932021115403.png",
                    FileName = "code-selfie932021115403.png"
                },
                new HomePage
                {
                    Id = 3,
                    Page = "Code",
                    Title = "Software Developer | C# .NET",
                    Intro = "Hello, thanks for checking out my portfolio. I'm a huge gamer nerd that loves coding.  Learning a new language or technology to solve a problem domain is basically studying mystic tongues to scribe spells.  HttpRequest, more like Neo-Electrical Telekinesis.  Writing code is wizardry, and who doesn't want to be a wizard?  As a long - time MMO end game raider and puzzler, I enjoy solving problems and organizing solutions.I find great satisfaction in laying out repository patterns in .NET or figuring out entity relations for a database.  When everything works out it's like beating a level of a game.  If things don't work out it's a learning experience until they do. While I enjoy puzzling and problem - solving I'm also a career artist with a passion for visual creation.  With a firm understanding of composition and color theory, I enjoy seeing projects through the full-stack process.",
                    Selfie = "https://falboportfoliostorage.blob.core.windows.net/images/code-selfie932021115248.png",
                    FileName = "code-selfie932021115248.png"
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

            modelBuilder.Entity<Project>().HasData(
                    new Project 
                    {
                        Id = 2,
                        Title = "LiteBerry Pi",
                        Description = "It does some things",
                        TechSummary = "I used these things",
                        RepoLink = "https://github.com/Lite-Berry-pi/Lite-Berry-Pi",
                        DeployedLink = "",
                        Order = 3,
                        Display = true,
                        AccordionId = "liteberrypi",
                        CollapseId = "liteberrypi2",
                        AdminAccordionId = "liteberrypiadmin",
                        AdminCollapseId = "liteberrypi2admin"
                    },
                    new Project
                    {
                        Id = 3,
                        Title = "React Minesweeper",
                        Description = "It does some things",
                        TechSummary = "I used these things",
                        RepoLink = "https://github.com/scottfalbo/react-minesweeper-v2",
                        DeployedLink = "https://scottfalbo.github.io/react-minesweeper-v2/",
                        Order = 2,
                        Display = true,
                        AccordionId = "reactminesweeper",
                        CollapseId = "reactminesweeper3",
                        AdminAccordionId = "reactminesweeperadmin",
                        AdminCollapseId = "reactminesweeper3admin"
                    },
                    new Project
                    {
                        Id = 4,
                        Title = "scottfalbo.com",
                        Description = "this",
                        TechSummary = "I used these things",
                        RepoLink = "https://github.com/scottfalbo",
                        DeployedLink = "",
                        Order = 1,
                        Display = true,
                        AccordionId = "scottfalbocom",
                        CollapseId = "scottfalbocom4",
                        AdminAccordionId = "scottfalbocomadmin",
                        AdminCollapseId = "scottfalbocom4admin"
                    }
                );

            // Add project images here

            string path = @"Data/technologiesData.json";
            TechnologiesList techList = JsonConvert.DeserializeObject<TechnologiesList>(File.ReadAllText(path));

            foreach (Technology tech in techList.technologies)
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
                    },
                    new ProjectTechnology
                    {
                        ProjectId = 3,
                        TechnologyId = tech.Id
                    },
                    new ProjectTechnology
                    {
                        ProjectId = 4,
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
    }

    public class TechnologiesList
    {
        public List<Technology> technologies { get; set; }
    }
}
