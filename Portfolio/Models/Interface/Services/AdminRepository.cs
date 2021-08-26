using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portfolio.Data;
using Portfolio.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        public async Task CreateProject(string title)
        {
            Project newProject = new Project()
            {
                Title = title,
                ImageUrl = "",
                Description = "",
                RepoLink = "",
                DeployedLink = "",
                Order = 0,
                AltText = title,
                Display = false
            };

            newProject = ProjectAccordionIds(newProject);

            _context.Entry(newProject).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Helper method to normalize the input title and use it for class identification with bootstrap accordion.
        /// </summary>
        /// <param name="project"> new project object </param>
        /// <returns> updated project object </returns>
        private Project ProjectAccordionIds(Project project)
        {
            string str = (Regex.Replace(project.Title, @"\s+", String.Empty)).ToLower();

            project.AccordionId = str;
            project.CollapseId = $"{str}{project.Id}";
            project.AdminAccordionId = $"{str}admin";
            project.AdminCollapseId = $"{str}{project.Id}admin";

            return project;
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
                .Include(z => z.Technologies)
                .ThenInclude(n => n.Technology)
                .Select(y => new Project
                {
                    Id = y.Id,
                    Title = y.Title,
                    ImageUrl = y.ImageUrl,
                    Description = y.Description,
                    TechSummary = y.TechSummary,
                    RepoLink = y.RepoLink,
                    DeployedLink = y.DeployedLink,
                    AltText = y.AltText,
                    Order = y.Order,
                    FileName = y.FileName,
                    Display = y.Display,
                    AccordionId = y.AccordionId,
                    CollapseId = y.CollapseId,
                    AdminAccordionId = y.AdminAccordionId,
                    AdminCollapseId = y.AdminCollapseId,
                    Technologies = y.Technologies
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
                .Include(z => z.Technologies)
                .ThenInclude(n => n.Technology)
                .Select(y => new Project
                {
                    Id = y.Id,
                    Title = y.Title,
                    ImageUrl = y.ImageUrl,
                    Description = y.Description,
                    TechSummary = y.TechSummary,
                    RepoLink = y.RepoLink,
                    DeployedLink = y.DeployedLink,
                    AltText = y.AltText,
                    Order = y.Order,
                    FileName = y.FileName,
                    Display = y.Display,
                    AccordionId = y.AccordionId,
                    CollapseId = y.CollapseId,
                    AdminAccordionId = y.AdminAccordionId,
                    AdminCollapseId = y.AdminCollapseId,
                    Technologies = y.Technologies
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
            BlobContainerClient container = new BlobContainerClient(Configuration["ImageBlob"], "images");
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

        /// <summary>
        /// Get studio data from DB
        /// </summary>
        /// <param name="id"> studio id </param>
        /// <returns> Studio object </returns>
        public async Task<Studio> GetStudio(int id)
        {
            return await _context.Studio
               .Where(x => x.Id == id)
               .Select(y => new Studio
               {
                   Id = y.Id,
                   Intro = y.Intro,
                   Policies = y.Policies,
                   Aftercare = y.Aftercare
               }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update studio data in database
        /// </summary>
        /// <param name="studio"> Studio object </param>
        public async Task UpdateStudio(Studio studio)
        {
            _context.Entry(studio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}