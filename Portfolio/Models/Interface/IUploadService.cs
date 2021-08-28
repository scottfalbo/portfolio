using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using System.IO;

namespace Portfolio.Models.Interface
{
    public interface IUploadService
    {
        public Task<BlobClient> UploadImage(IFormFile file);
        public Task<BlobClient> UploadImage(Stream stream, string filename, string contentType);
        public Task<bool> CheckFileName(IFormFile file);

        public Task<Image> AddArtImage(IFormFile file);
        public Task<Image> AddProjectImage(IFormFile file);


        public Task UpdateSelfie(IFormFile file, int id);
    }
}
