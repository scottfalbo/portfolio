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

        /// <summary>
        /// Instantiate a new Gallery() object and add it to the database
        /// </summary>
        /// <param name="gallery"></param>
        /// <returns></returns>
        public async Task CreateGallery(Gallery gallery)
        {
            Gallery newGallery = new Gallery()
            {
                Title = gallery.Title,
                Display = false,
                Order = gallery.Order
            };
            _context.Entry(newGallery).State = EntityState.Added;
            await _context.SaveChangesAsync();
            await GalleryCollapseId(gallery);
        }
        // Helper method to add a CollapseId to the gallery for use with bootstrap accordian
        private async Task GalleryCollapseId(Gallery gallery)
        {
            Gallery newGallery = await _context.Galleries
                .Where(x => gallery.Title == x.Title)
                .Select(y => new Gallery
                {
                    Id = y.Id,
                    Title = y.Title,
                    CollapseId = $"{y.Title}{y.Id}",
                    Display = y.Display,
                    Order = y.Order
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get a gallery by Id from database
        /// </summary>
        /// <param name="id"> gallery Id </param>
        /// <returns> Gallery object </returns>
        public async Task<Gallery> GetGallery(int id)
        {
            return await _context.Galleries
                .Where(x => x.Id == id)
                .Include(gi => gi.GalleryImages)
                .ThenInclude(i => i.Image)
                .Select(y => new Gallery
                {
                    Id = y.Id,
                    Title = y.Title,
                    Display = y.Display,
                    Order = y.Order,
                    AccordianId = y.AccordianId,
                    CollapseId = y.CollapseId,
                    GalleryImages = y.GalleryImages
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all of the galleries from the database
        /// </summary>
        /// <returns> List<Gallery> /Gallery></returns>
        public async Task<List<Gallery>> GetGalleries()
        {
            return await _context.Galleries
                .Include(gi => gi.GalleryImages)
                .ThenInclude(i => i.Image)
                .Select(y => new Gallery
                {
                    Id = y.Id,
                    Title = y.Title,
                    Display = y.Display,
                    Order = y.Order,
                    AccordianId = y.AccordianId,
                    CollapseId = y.CollapseId,
                    GalleryImages = y.GalleryImages
                }).ToListAsync();
        }

        /// <summary>
        /// Update a gallery in the database
        /// </summary>
        /// <param name="gallery"> Gallery() object </param>
        public async Task UpdateGallery(Gallery gallery)
        {
            _context.Entry(gallery).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a gallery and all GalleryImages and Images associated
        /// </summary>
        /// <param name="id"> gallery id </param>
        public  async Task DeleteGallery(int id)
        {
            Gallery gallery = await GetGallery(id);
            List<int> imageIds = new List<int>();
            foreach (GalleryImage image in gallery.GalleryImages)
            {
                imageIds.Add(image.ImageId);
                await RemoveImageFromGallery(id, image.ImageId);
            }
            foreach (int image in imageIds)
                await DeleteImage(image);
            _context.Entry(gallery).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add an image to a gallery using the GalleryImage join table
        /// </summary>
        /// <param name="galleryId"> gallery id </param>
        /// <param name="imageId"> image id </param>
        public async Task AddImageToGallery(int galleryId, int imageId)
        {
            GalleryImage galleryImage = new GalleryImage()
            {
                GalleryId = galleryId,
                ImageId = imageId
            };
            _context.Entry(galleryImage).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a GalleryImage join table from database
        /// </summary>
        /// <param name="galleryId"> gallery id</param>
        /// <param name="imageId"> image id </param>
        public async Task RemoveImageFromGallery(int galleryId, int imageId)
        {
            GalleryImage image = await _context.GalleryImage
                .Where(x => x.GalleryId == galleryId && x.ImageId == imageId)
                .Select(y => new GalleryImage
                {
                    GalleryId = y.GalleryId,
                    ImageId = y.ImageId
                }).FirstOrDefaultAsync();
            _context.Entry(image).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
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
