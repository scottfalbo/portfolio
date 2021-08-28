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
        public IArtAdmin _art;

        public AdminRepository(PortfolioDbContext context, IConfiguration config, IArtAdmin art)
        {
            _context = context;
            Configuration = config;
            _art = art;
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

            List<Technology> techlist = await GetTechnologies();
            foreach (var tech in techlist)
                await AddTechToProject(newProject.Id, tech.Id);
        }

        /// <summary>
        /// Checks for duplicate project names on creation.
        /// </summary>
        /// <param name="title"> string title input </param>
        /// <returns> true if repeat </returns>
        public async Task<bool> CheckProjectTitle(string title)
        {
            Project project = await _context.Projects
                .Where(x => x.Title == title)
                .Select(y => new Project
                {
                    Id = y.Id,
                    Title = y.Title
                }).FirstOrDefaultAsync();

            return project != null;
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
                .ThenInclude(a => a.Technology)
                .Include(b => b.ProjectImages)
                .ThenInclude(c => c.Image)
                .Select(y => new Project
                {
                    Id = y.Id,
                    Title = y.Title,
                    Description = y.Description,
                    TechSummary = y.TechSummary,
                    RepoLink = y.RepoLink,
                    DeployedLink = y.DeployedLink,
                    AltText = y.AltText,
                    Order = y.Order,
                    Display = y.Display,
                    AccordionId = y.AccordionId,
                    CollapseId = y.CollapseId,
                    AdminAccordionId = y.AdminAccordionId,
                    AdminCollapseId = y.AdminCollapseId,
                    Technologies = y.Technologies,
                    ProjectImages = y.ProjectImages
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
                .Include(x => x.Technologies)
                .ThenInclude(a => a.Technology)
                .Include(b => b.ProjectImages)
                .ThenInclude(c => c.Image)
                .Select(y => new Project
                {
                    Id = y.Id,
                    Title = y.Title,
                    Description = y.Description,
                    TechSummary = y.TechSummary,
                    RepoLink = y.RepoLink,
                    DeployedLink = y.DeployedLink,
                    AltText = y.AltText,
                    Order = y.Order,
                    Display = y.Display,
                    AccordionId = y.AccordionId,
                    CollapseId = y.CollapseId,
                    AdminAccordionId = y.AdminAccordionId,
                    AdminCollapseId = y.AdminCollapseId,
                    Technologies = y.Technologies,
                    ProjectImages = y.ProjectImages
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

            if (project.ProjectImages != null)
            {
                foreach (var image in project.ProjectImages)
                {
                    await RemoveImageFromProject(id, image.Image.Id);
                    await _art.DeleteImage(image.Image.Id);
                }
            }

            _context.Entry(project).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds an image to a project by adding a ProjectImage join table record.
        /// </summary>
        /// <param name="projectId"> int project id</param>
        /// <param name="imageId"> int image id </param>
        public async Task AddImageToProject(int projectId, int imageId)
        {
            ProjectImage projectImage = new ProjectImage()
            {
                ProjectId = projectId,
                ImageId = imageId
            };
            _context.Entry(projectImage).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an image from a project by deleting the ProjectImage join table record.
        /// </summary>
        /// <param name="projectId"> int project id</param>
        /// <param name="imageId"> int image id </param>
        public async Task RemoveImageFromProject(int projectId, int imageId)
        {
            ProjectImage image = await _context.ProjectImages
                .Where(x => x.ProjectId == projectId && x.ImageId == imageId)
                .Select(y => new ProjectImage
                {
                    ProjectId = y.ProjectId,
                    ImageId = y.ImageId
                }).FirstOrDefaultAsync();
            _context.Entry(image).State = EntityState.Deleted;
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
                .Include(a => a.Technologies)
                .ThenInclude(b => b.Technology)
                .Select(y => new HomePage
                {
                    Id = y.Id,
                    Page = y.Page,
                    Title = y.Title,
                    Intro = y.Intro,
                    Selfie = y.Selfie,
                    FileName = y.FileName,
                    Technologies = y.Technologies
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Return a list of all homepages
        /// </summary>
        /// <returns> List<HomePage> </returns>
        public async Task<List<HomePage>> GetHomePages()
        {
            return await _context.HomePage
                .Include(a => a.Technologies)
                .ThenInclude(b => b.Technology)
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

        /// <summary>
        /// Retrieve my list of technology icons from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Technology>> GetTechnologies()
        {
            return await _context.Technologies
                .Select(x => new Technology
                {
                    Id = x.Id,
                    Title = x.Title,
                    LogoUrl = x.LogoUrl
                }).ToListAsync();
        }

        /// <summary>
        /// Add a technology to a project.
        /// </summary>
        /// <param name="projectId"> int project id </param>
        /// <param name="techId"> int tech id </param>
        public async Task AddTechToProject(int projectId, int techId)
        {
            ProjectTechnology newTech = new ProjectTechnology()
            {
                ProjectId = projectId,
                TechnologyId = techId
            };
            _context.Entry(newTech).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a technology form a project.
        /// </summary>
        /// <param name="projectId"> int project id </param>
        /// <param name="techId"> int tech id </param>
        public async Task RemoveTechFromProject(int projectId, int techId)
        {
            ProjectTechnology tech = await _context.ProjectTechnologies
                .Where(x => x.ProjectId == projectId && x.TechnologyId == techId)
                .Select(y => new ProjectTechnology
                {
                    ProjectId = y.ProjectId,
                    TechnologyId = y.TechnologyId
                }).FirstOrDefaultAsync();
            _context.Entry(tech).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}