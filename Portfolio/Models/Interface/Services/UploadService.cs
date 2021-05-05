using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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

        public UploadService(IConfiguration config, IAdmin admin)
        {
            Configuration = config;
            _admin = admin;
        }

        /// <summary>
        /// Uploads the image to azure blob storage
        /// Creates a new project in the database with the new image url as the sourceUrl
        /// </summary>
        /// <param name="file"> file to upload </param>
        public async Task UploadImage(IFormFile file)
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

            Project newProject = new Project() 
            {
                Title = "new",
                Description = "new project",
                SourceURL = blob.Uri.ToString() ,
                FileName = file.FileName
            };

            await _admin.CreateProject(newProject);
        }


    }
}
