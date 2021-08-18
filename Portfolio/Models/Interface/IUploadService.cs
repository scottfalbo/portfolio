using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interface
{
    public interface IUploadService
    {
        public Task<BlobClient> UploadImage(IFormFile file);

        public Task AddProjectImage(IFormFile file);
        public Task<Image> AddArtImage(IFormFile file);

        public Task UpdateProjectImage(IFormFile file, int id);
        public Task UpdateArtImage(IFormFile file, int id);
        public Task UpdateSelfie(IFormFile file, int id);
    }
}
