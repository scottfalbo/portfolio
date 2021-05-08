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
        private readonly PortfolioDbContext _context;

        public UploadService(IConfiguration config, IAdmin admin, PortfolioDbContext context)
        {
            Configuration = config;
            _admin = admin;
            _context = context;
        }

        /// <summary>
        /// Uploads the image to azure blob storage
        /// </summary>
        /// <param name="file"> file to upload </param>
        /// <returns> new BlobClient object </returns>
        public async Task<BlobClient> UploadImage(IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(Configuration.GetConnectionString("ImageBlob"), "images");

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
        /// Updates a projects sourceURL to newly uploaded image
        /// </summary>
        /// <param name="file"> input file </param>
        /// <param name="id"> project id </param>
        /// <returns> no return </returns>
        public async Task UpdateImage(IFormFile file, int id)
        {
            Project project = await _context.Projects.FindAsync(id);
            BlobClient blob = await UploadImage(file);
            project.SourceURL = blob.Uri.ToString();
            project.FileName = file.FileName;
            await _admin.UpdateProject(project);
        }
    }
}
