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
                    Description = "SmallBoi is a two player coop platform puzzle game built in Unity.  It has both local and network multiple player options using Photon.",
                    RepoLink = "https://github.com/AmeiliaAndTheSmallBois/SmallBoi/tree/main"
                },
                new Project
                {
                    Id = 2,
                    Title = "LiteBerry Pi",
                    SourceURL = "images/liteberrypi.png",
                    Description = "LiteBerry Pi allows users to create and send designs to a RaspBerry Pi with a matrix of led lights attached.  The app uses an api to create and save designs.  The api also contains a route to send designs to the Pi using a SignalR server.",
                    RepoLink = "https://github.com/Lite-Berry-pi/Lite-Berry-Pi"
                },
                new Project
                {
                    Id = 3,
                    Title = "React Minesweeper",
                    SourceURL = "images/minesweeper.png",
                    Description = "A re-creation of the Window's classic Minesweeper.  I built this to practice components and state within a React App.",
                    RepoLink = "https://github.com/scottfalbo/react-minesweeper-v2",
                    DeployedLink = "https://scottfalbo.github.io/react-minesweeper-v2/"
                }
            );
        }
    }
}
