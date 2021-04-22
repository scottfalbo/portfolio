using Microsoft.Data.Sqlite;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using System.Threading.Tasks;
using System.Linq;

namespace PortfolioTests
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly PortfolioDbContext _db;

        /// <summary>
        /// Constructor to create a mock database based on PortfolioDbContext.
        /// </summary>
        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new PortfolioDbContext(
                new DbContextOptionsBuilder<PortfolioDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        /// <summary>
        /// Self destruct method for the database after use.
        /// </summary>
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }

        protected async Task CreateProject(Project project)
        {
            Project newProject = new Project()
            {
                Title = project.Title,
                SourceURL = project.SourceURL,
                Description = project.Description,
                RepoLink = project.RepoLink,
                DeployedLink = project.DeployedLink,
                Order = project.Order,
                AltText = project.AltText
            };
            _db.Entry(newProject).State = EntityState.Added;
            await _db.SaveChangesAsync();
        }

        protected async Task<Project> GetProject(int id)
        {
            return await _db.Projects
                .Where(x => x.Id == id)
                .Select(y => new Project
                {
                    Title = y.Title,
                    SourceURL = y.SourceURL,
                    Description = y.Description,
                    RepoLink = y.RepoLink,
                    DeployedLink = y.DeployedLink,
                    AltText = y.AltText,
                    Order = y.Order
                })
                .FirstOrDefaultAsync();
        }
    }
}
