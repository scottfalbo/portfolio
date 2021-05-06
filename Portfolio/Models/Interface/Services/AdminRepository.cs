using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
                AltText = project.AltText
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
                    Order = y.Order
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
                    Order = x.Order
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
            Project newProject = new Project()
            {
                Id = project.Id,
                Title = project.Title,
                SourceURL = project.SourceURL,
                Description = project.Description,
                RepoLink = project.RepoLink,
                DeployedLink = project.DeployedLink,
                AltText = project.AltText,
                Order = project.Order
            };

            _context.Entry(newProject).State = EntityState.Modified;
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

            BlobContainerClient container = new BlobContainerClient(Configuration.GetConnectionString("ImageBlob"), "images");

            await container.CreateIfNotExistsAsync();

            BlobClient blob = container.GetBlobClient(project.FileName);

            await blob.DeleteIfExistsAsync();

            //var sasToken = Configuration.GetConnectionString("BlobSASToken");
            //Uri uri = new Uri(project.SourceURL);
            //var blobUri = new Uri(uri, project.FileName + sasToken);
            //BlobClient blob = new BlobClient(blobUri);
            //await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

            _context.Entry(project).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
