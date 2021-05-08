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

        public async Task CreateTattoo(Tattoo tattoo)
        {
            Tattoo newTattoo = new Tattoo()
            {
                ImageURL = tattoo.ImageURL,
                FileName = tattoo.FileName,
                Display = false
            };
            _context.Entry(newTattoo).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<Tattoo> GetTattoo(int id)
        {
            return await _context.Tattoos
                .Where(x => x.Id == id)
                .Select(y => new Tattoo
                {
                    Id = y.Id,
                    ImageURL = y.ImageURL,
                    FileName = y.FileName,
                    Order = y.Order,
                    Display = y.Display
                }).FirstOrDefaultAsync();
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

        public async Task UpdateTattoo(Tattoo tattoo)
        {
            _context.Entry(tattoo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTattoo(int id)
        {
            Tattoo tattoo = await _context.Tattoos.FindAsync(id);

            await DeleteBlobImage(tattoo.FileName);

            _context.Entry(tattoo).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
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

        public Task UpdateDrawing(Drawing drawing)
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
