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
    }
}
