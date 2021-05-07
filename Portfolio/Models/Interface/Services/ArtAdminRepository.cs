using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interface.Services
{
    public class ArtAdminRepository : IArtAdmin
    {
        private readonly PortfolioDbContext _context;
        public IConfiguration Configuration { get; }

        public ArtAdminRepository(PortfolioDbContext context, IConfiguration config)
        {
            _context = context;
            Configuration = config;
        }

        public Task CreateTattoo(Tattoo tattoo)
        {
            throw new NotImplementedException();
        }

        public Task<Tattoo> GetTattoo(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Tattoo>> GetTattoos()
        {
            return await _context.Tattoos
                .Select(x => new Tattoo
                {
                    Id = x.Id,
                    ImageURL = x.ImageURL,
                    FileName = x.FileName,
                    Display = x.Display
                })
                .ToListAsync();
        }

        public Task<Tattoo> UpdateTattoo(Tattoo tattoo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTattoo(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllTattoos()
        {
            throw new NotImplementedException();
        }

        public Task CreateDrawing(Drawing drawing)
        {
            throw new NotImplementedException();
        }

        public Task<Drawing> GetDrawing(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Drawing>> GetDrawings()
        {
            throw new NotImplementedException();
        }

        public Task<Drawing> UpdateDrawing(Drawing drawing)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDrawing(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllDrawings()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes file from azure storage
        /// </summary>
        /// <param name="fileName"> file to delete </param>
        /// <returns> no return </returns>
        public async Task DeleteBlobImage(string fileName)
        {
            BlobContainerClient container = new BlobContainerClient(Configuration.GetConnectionString("ImageBlob"), "images");
            BlobClient blob = container.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, null, default);
        }
    }
}
