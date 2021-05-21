using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using InstagramApiSharp.API.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portfolio.Data;
using Portfolio.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interfaces.Services
{
    public class AdminRepository : IAdmin
    {
        private readonly PortfolioDbContext _context;
        public IConfiguration Configuration { get; }

        public AdminRepository(PortfolioDbContext context, IConfiguration config)
        {
            _context = context;
            Configuration = config;
        }

        /// <summary>
        /// Create a new project entry in the database
        /// </summary>
        /// <param name="project"> project object from form input </param>
        /// <returns> no return </returns>
        public async Task CreateProject(Project project)
        {
            Project newProject = new Project()
            {
                Title = project.Title,
                SourceURL = project.SourceURL,
                Description = project.Description,
                RepoLink = project.RepoLink,
                DeployedLink = project.DeployedLink,
                Order = project.Order,
                AltText = project.AltText,
                FileName = project.FileName,
                Display = project.Display
            };
            _context.Entry(newProject).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Read a project by id from the database
        /// </summary>
        /// <param name="id"> project id </param>
        /// <returns> Project object </returns>
        public async Task<Project> GetProject(int id)
        {
            return await _context.Projects
                .Where(x => x.Id == id)
                .Select(y => new Project
                {
                    Id = y.Id,
                    Title = y.Title,
                    SourceURL = y.SourceURL,
                    Description = y.Description,
                    RepoLink = y.RepoLink,
                    DeployedLink = y.DeployedLink,
                    AltText = y.AltText,
                    Order = y.Order,
                    FileName = y.FileName,
                    Display = y.Display
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Read all projects in the database
        /// </summary>
        /// <returns> List of Project objects </returns>
        public async Task<List<Project>> GetProjects()
        {
            return await _context.Projects
                .Select(x => new Project
                {
                    Id = x.Id,
                    Title = x.Title,
                    SourceURL = x.SourceURL,
                    Description = x.Description,
                    RepoLink = x.RepoLink,
                    DeployedLink = x.DeployedLink,
                    AltText = x.AltText,
                    Order = x.Order,
                    FileName = x.FileName,
                    Display = x.Display
                })
                .ToListAsync();
        }

        /// <summary>
        /// Update a project in the database
        /// </summary>
        /// <param name="id"> project id </param>
        /// <param name="project"> Project object from form input </param>
        /// <returns> no return </returns>
        public async Task UpdateProject(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a project from the database and remove image from azure blob storage
        /// </summary>
        /// <param name="id"> project id </param>
        /// <returns> no return </returns>
        public async Task DeleteProject(int id)
        {
            Project project = await _context.Projects.FindAsync(id);

            await DeleteBlobImage(project.FileName);

            _context.Entry(project).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the image from azure storage
        /// </summary>
        /// <param name="fileName"> image file name </param>
        /// <returns> no return </returns>
        public async Task DeleteBlobImage(string fileName)
        {
            BlobContainerClient container = new BlobContainerClient(Configuration.GetConnectionString("ImageBlob"), "images");
            BlobClient blob = container.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, null, default);
        }

        /// <summary>
        /// Get HomePage data from the database
        /// </summary>
        /// <returns> HomePage object </returns>
        public async Task<HomePage> GetHomePage(string page)
        {
            return await _context.HomePage
                .Where(x => x.Page == page)
                .Select(y => new HomePage
                {
                    Id = y.Id,
                    Page = y.Page,
                    Title = y.Title,
                    Intro = y.Intro,
                    Selfie = y.Selfie,
                    FileName = y.FileName
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Return a list of all homepages
        /// </summary>
        /// <returns> List<HomePage> </returns>
        public async Task<List<HomePage>> GetHomePages()
        {
            return await _context.HomePage
                .Select(x => new HomePage
                {
                    Id = x.Id,
                    Page = x.Page,
                    Title = x.Title,
                    Intro = x.Intro,
                    Selfie = x.Selfie,
                    FileName = x.FileName
                }).ToListAsync();
        }

        /// <summary>
        /// Update HomePage Title and Intro data and save to database
        /// </summary>
        /// <param name="homepage"> HomePage object </param>
        public async Task UpdateHomePage(HomePage homepage)
        {
            _context.Entry(homepage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Instagram>> GetInstagram()
        {
            string accessToken = "AQA4w-FPtxdt7ABeIBkiAkucemIGwzWacjNwIDnuKO8eLohO2AYg6sDP8hX9C4p1VK4fCK9wVapr8RyO7PHEQu6z73EUVeOktv2FVaUz3zl0r29dr6bwooJJtBXa6G-hNWAMewABuzLihCe2pi7dinbaoa-JrlbCVfT7NDj0D5oFimO1uPkQt3QXzCLv5o2yHJ6lLa7YxjTQha3o_xd8D5QyI7YURX4wwKDp2-ueCzJxyg";


            Console.WriteLine("");
            return null;
        }
    }
}
