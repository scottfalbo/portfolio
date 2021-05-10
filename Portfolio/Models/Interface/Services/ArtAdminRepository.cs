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

        //-------------------------------Tattoo CRUD ------------------------------
        /// <summary>
        /// Instantiate a new Tattoo() object and save it to the database
        /// </summary>
        /// <param name="tattoo"> new Tattoo object </param>
        public async Task CreateTattoo(Tattoo tattoo)
        {
            Tattoo newTattoo = new Tattoo()
            {
                ImageURL = tattoo.ImageURL,
                FileName = tattoo.FileName,
                Order = 0,
                Display = false
            };
            _context.Entry(newTattoo).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get a Tattoo object by Id from the database
        /// </summary>
        /// <param name="id"> tattoo id </param>
        /// <returns> Tattoo object </returns>
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

        /// <summary>
        /// Get a list of all Tattoo objects contained in the database
        /// </summary>
        /// <returns> List<Tattoo> </returns>
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

        /// <summary>
        /// Update a Tattoo objects and save to database
        /// </summary>
        /// <param name="tattoo"> updated Tattoo object </param>
        public async Task UpdateTattoo(Tattoo tattoo)
        {
            _context.Entry(tattoo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a Tattoo object from the database
        /// Remove the associated image from azure storage
        /// </summary>
        /// <param name="id"> tattoo id </param>
        public async Task DeleteTattoo(int id)
        {
            Tattoo tattoo = await _context.Tattoos.FindAsync(id);

            await DeleteBlobImage(tattoo.FileName);

            _context.Entry(tattoo).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete all of the saved tattoos
        /// </summary>
        public async Task DeleteAllTattoos()
        {
            List<Tattoo> tattoos = await GetTattoos();
            foreach (Tattoo tattoo in tattoos)
                await DeleteTattoo(tattoo.Id);
        }

        //-------------------------------Drawing CRUD ------------------------------
        /// <summary>
        /// Instantiate a new Drawing object with image data and save to database
        /// </summary>
        /// <param name="drawing"> drawing Object </param>
        public async Task CreateDrawing(Drawing drawing)
        {
            Drawing newDrawing = new Drawing()
            {
                ImageURL = drawing.ImageURL,
                FileName = drawing.FileName,
                Order = 0,
                Display = false
            };
            _context.Entry(newDrawing).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get a drawing by Id from the database
        /// </summary>
        /// <param name="id"> drawing id </param>
        /// <returns> Drawing object </returns>
        public async Task<Drawing> GetDrawing(int id)
        {
            return await _context.Drawings
                .Where(x => x.Id == id)
                .Select(y => new Drawing
                {
                    Id = y.Id,
                    ImageURL = y.ImageURL,
                    FileName = y.FileName,
                    Order = y.Order,
                    Display = y.Display
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get and return a list of all of the drawings in the database
        /// </summary>
        /// <returns> List<Drawing> drawings </returns>
        public async Task<List<Drawing>> GetDrawings()
        {
            return await _context.Drawings
                .Select(y => new Drawing
                {
                    Id = y.Id,
                    ImageURL = y.ImageURL,
                    FileName = y.FileName,
                    Order = y.Order,
                    Display = y.Display
                }).ToListAsync();
        }

        /// <summary>
        /// Save updated drawing object to the database
        /// </summary>
        /// <param name="drawing"> updated Drawing object </param>
        public async Task UpdateDrawing(Drawing drawing)
        {
            _context.Entry(drawing).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public Task DeleteDrawing(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllDrawings()
        {
            throw new NotImplementedException();
        }

        //------------------------------- Shared ------------------------------
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
