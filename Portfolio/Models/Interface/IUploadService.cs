using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interface
{
    interface IUploadService
    {
        Task<string> UploadImage(IFormFile file);
    }
}
