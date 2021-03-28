using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public class PortfolioDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public PortfolioDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = -1,
                    Title = "SmallBoi, The Game",
                    SourceURL = "images/smallboi.png",
                    Description = "add some words here",
                    RepoLink = "https://github.com/AmeiliaAndTheSmallBois/SmallBoi/tree/main"
                },
                new Project
                {
                    Id = 2,
                    Title = "LiteBerry Pi",
                    SourceURL = "images/liteberrypi.png",
                    Description = "blah blah blah blah signalR",
                    RepoLink = "https://github.com/Lite-Berry-pi/Lite-Berry-Pi"
                },
                new Project
                {
                    Id = 3,
                    Title = "React Minesweeper",
                    SourceURL = "images/minesweeper.png",
                    Description = "it's minesweeper",
                    RepoLink = "https://github.com/scottfalbo/react-minesweeper-v2",
                    DeployedLink = "https://scottfalbo.github.io/react-minesweeper-v2/"
                }
            );
        }
    }
}
