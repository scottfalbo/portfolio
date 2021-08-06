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

        //-------------------------------Image CRUD ------------------------------
        /// <summary>
        /// Instantiate a new Image() object and save it to the database
        /// </summary>
        /// <param name="image"> new Image object </param>
        public async Task CreateImage(Image image)
        {
            Image newImage = new Image()
            {
                Title = image.FileName,
                Type = image.Type,
                ImageURL = image.ImageURL,
                FileName = image.FileName,
                Order = 0
            };
            _context.Entry(newImage).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get a Image object by Id from the database
        /// </summary>
        /// <param name="id"> image id </param>
        /// <returns> Image object </returns>
        public async Task<Image> GetImage(int id)
        {
            return await _context.Images
                .Where(x => x.Id == id)
                .Select(y => new Image
                {
                    Id = y.Id,
                    Title = y.Title,
                    Type = y.Type,
                    ImageURL = y.ImageURL,
                    FileName = y.FileName,
                    Order = y.Order
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get a list of all Image objects contained in the database
        /// </summary>
        /// <returns> List<Image> </returns>
        public async Task<List<Image>> GetImages()
        {
            return await _context.Images
                .Select(x => new Image
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageURL = x.ImageURL,
                    FileName = x.FileName,
                    Type = x.Type,
                    Order = x.Order
                })
                .ToListAsync();
        }

        /// <summary>
        /// Update a Image objects and save to database
        /// </summary>
        /// <param name="image"> updated Image object </param>
        public async Task UpdateImage(Image image)
        {
            _context.Entry(image).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a Image object from the database
        /// Remove the associated image from azure storage
        /// </summary>
        /// <param name="id"> image id </param>
        public async Task DeleteImage(int id)
        {
            Image image = await _context.Images.FindAsync(id);

            await DeleteBlobImage(image.FileName);

            _context.Entry(image).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete all saved image objects and galleries
        /// </summary>
        public async Task DeleteAll()
        {
            List<Image> tattoos = await GetImages();
            foreach (Image tattoo in tattoos)
                await DeleteImage(tattoo.Id);
        }

        public Task CreateGallery(Gallery gallery)
        {
            throw new NotImplementedException();
        }
        public Task<Image> GetGallery(int id)
        {
            throw new NotImplementedException();
        }
        public Task<List<Image>> GetGalleries()
        {
            throw new NotImplementedException();
        }
        public Task UpdateGallery(Image tattoo)
        {
            throw new NotImplementedException();
        }
        public Task DeleteGallery(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddImageToGallery(int galleryId, int imageId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveImageFromGallery(int galleryId, int imageId)
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
            BlobContainerClient container = new BlobContainerClient(Configuration["ImageBlob"], "images");
            BlobClient blob = container.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, null, default);
        }

    }
}
