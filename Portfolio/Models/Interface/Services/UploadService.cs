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

        public async Task<string> UploadImage(IFormFile file)
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

            return blob.Uri.ToString();
        }
    }
}
