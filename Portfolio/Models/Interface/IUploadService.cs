﻿using Azure.Storage.Blobs;
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
        public Task AddTattooImage(IFormFile file);
        public Task AddDrawingImage(IFormFile file);

        public Task UpdateImage(IFormFile file, int id);
        public Task UpdateTattooImage(IFormFile file, int id);
        public Task UpdateSelfie(IFormFile file, int id);
    }
}
