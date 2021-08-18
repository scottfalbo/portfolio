using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interface.Services
{
    public class UploadService : IUploadService
    {
        public IConfiguration Configuration { get; }
        public IAdmin _admin;
        public IArtAdmin _artAdmin;
        private readonly PortfolioDbContext _context;

        public UploadService(IConfiguration config, IAdmin admin, PortfolioDbContext context, IArtAdmin art)
        {
            Configuration = config;
            _admin = admin;
            _context = context;
            _artAdmin = art;
        }

        /// <summary>
        /// Uploads the image to azure blob storage
        /// </summary>
        /// <param name="file"> file to upload </param>
        /// <returns> new BlobClient object </returns>
        public async Task<BlobClient> UploadImage(IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(Configuration["ImageBlob"], "images");

            await container.CreateIfNotExistsAsync();

                BlobClient blob = container.GetBlobClient(file.FileName);

                using var stream = file.OpenReadStream();

                BlobUploadOptions options = new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
                };

                if (!blob.Exists())
                    await blob.UploadAsync(stream, options);

                return blob;
        }

        /// <summary>
        /// Upload an image and create a new project
        /// </summary>
        /// <param name="file"> input file </param>
        /// <returns> no return </returns>
        public async Task AddProjectImage(IFormFile file)
        {
            BlobClient blob = await UploadImage(file);

            Project newProject = new Project()
            {
                Title = "new",
                Description = "new project",
                SourceURL = blob.Uri.ToString(),
                FileName = file.FileName,
                Display = false
            };
            await _admin.CreateProject(newProject);
        }

        /// <summary>
        /// Upload and image to azure storage
        /// Create a new Tattoo object with new image and save to database
        /// </summary>
        /// <param name="file"> input file </param>
        public async Task<Image> AddArtImage(IFormFile file)
        {
            BlobClient blob = await UploadImage(file);

            Image image = new Image()
            {
                ImageURL = blob.Uri.ToString(),
                FileName = file.FileName,
                ThumbURL = blob.Uri.ToString(),
                ThumbFileName = file.FileName,
                Order = 0
            };
            return await _artAdmin.CreateImage(image);
        }

        /// <summary>
        /// Updates a projects sourceURL to newly uploaded image
        /// </summary>
        /// <param name="file"> input file </param>
        /// <param name="id"> project id </param>
        /// <returns> no return </returns>
        public async Task UpdateProjectImage(IFormFile file, int id)
        {
            Project project = await _context.Projects.FindAsync(id);
            BlobClient blob = await UploadImage(file);
            project.SourceURL = blob.Uri.ToString();
            project.FileName = file.FileName;
            await _admin.UpdateProject(project);
        }

        /// <summary>
        /// Updates a tattoos ImageUrl to newly uploaded image
        /// </summary>
        /// <param name="file"> input file </param>
        /// <param name="id"> tattoo id </param>
        public async Task UpdateArtImage(IFormFile file, int id)
        {
            Image tattoo = await _context.Images.FindAsync(id);
            BlobClient blob = await UploadImage(file);
            tattoo.ImageURL = blob.Uri.ToString();
            tattoo.FileName = file.FileName;
            await _artAdmin.UpdateImage(tattoo);
        }

        /// <summary>
        /// Updates the HomePage selfie to new image
        /// </summary>
        /// <param name="file"> input file </param>
        /// <param name="id"> homepage id </param>
        public async Task UpdateSelfie(IFormFile file, int id)
        {
            HomePage homepage = await _context.HomePage.FindAsync(id);
            BlobClient blob = await UploadImage(file);
            homepage.Selfie = blob.Uri.ToString();
            homepage.FileName = file.FileName;
            await _admin.UpdateHomePage(homepage);
        }
    }
}
