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

        /// --------------- Instagram API
        /// <summary>
        /// Makes a request to the Instagram api.  First retrieves a list of recent post ids.
        /// Calls a helper method to loop through ids and request media data for each.
        /// Call another helper to Instantiate Instagram objects and add to database.
        /// </summary>
        /// <returns></returns>
        public async Task GetInstagramFeed()
        {
            string userId = Configuration["Instagram:UserId"];
            string accessToken = Configuration["Instagram:AccessToken"];

            using var client = new HttpClient();
            string uri = $"https://graph.instagram.com/{userId}/media?access_token={accessToken}";
            var content = await client.GetAsync(uri);

            Root readImages = new Root();
            if (content.IsSuccessStatusCode.Equals(true))
                readImages = await content.Content.ReadAsAsync<Root>();

            await GetInstagramMedia(readImages);
        }

        /// <summary>
        /// Helper method to loop through media ids and request urls for each.
        /// </summary>
        /// <param name="imageIds"> Root object from user API call </param>
        public async Task GetInstagramMedia(Root imageIds)
        {
            string accessToken = Configuration["Instagram:AccessToken"];
            List<InstaMedia> image_urls = new List<InstaMedia>();

            foreach (var image in imageIds.data)
            {
                using var client = new HttpClient();
                string uri = $"https://graph.instagram.com/{image.id}?access_token={accessToken}&fields=media_url,media_type";
                var content = await client.GetAsync(uri);

                if (content.IsSuccessStatusCode.Equals(true))
                {
                    InstaMedia mediaContent = await content.Content.ReadAsAsync<InstaMedia>();
                    image_urls.Add(mediaContent);
                }
            }
            await UpdateInstagramDB(image_urls);
        }

        /// <summary>
        /// Remove all old content from database and add current feed data.
        /// </summary>
        /// <param name="image_urls"> List of media_urls </param>
        public async Task UpdateInstagramDB(List<InstaMedia> image_urls)
        {
            await DeleteInstagrams();
            foreach(var image in image_urls)
            {
                Instagram newInsta = new Instagram 
                { 
                    ImageURL = image.media_url
                };
                _context.Entry(newInsta).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Return a list of all Instagram objects in database.
        /// </summary>
        /// <returns> List<Instagram> </returns>
        public async Task<List<Instagram>> GetInstagrams()
        {
            return await _context.Instragrams
                .Select(x => new Instagram
                {
                    Id = x.Id,
                    ImageURL = x.ImageURL
                })
                .ToListAsync();
        }

        /// <summary>
        /// Delete all instagram objects in the database to refresh feed.
        /// </summary>
        public async Task DeleteInstagrams()
        {
            List<Instagram> insta = await GetInstagrams();
            foreach(var item in insta)
            {
                _context.Entry(item).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public void RefreshAccessToken()
        {
            //refresh token every 60 days
        }
    }

    /// <summary>
    /// Constructor classes for the Instragram API result objects
    /// Generated by https://json2csharp.com/
    /// </summary>
    public class Datum
    {
        public string id { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class Root
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }

    public class InstaMedia
    {
        public string media_url { get; set; }
        public string media_type { get; set; }
        public string id { get; set; }
    }
}
