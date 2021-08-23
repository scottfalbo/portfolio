using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public async Task<Image> CreateImage(Image image)
        {
            Image newImage = new Image()
            {
                Title = image.FileName,
                ImageURL = image.ImageURL,
                FileName = image.FileName,
                ThumbURL = image.ThumbURL,
                ThumbFileName = image.ThumbFileName,
                Order = 0
            };
            _context.Entry(newImage).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return newImage;
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
                    ThumbURL = y.ThumbURL,
                    ThumbFileName = y.ThumbFileName,
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
                    ThumbURL = x.ThumbURL,
                    ThumbFileName = x.ThumbFileName,
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
        public async Task CreateGallery(string title)
        {
            Gallery newGallery = new Gallery()
            {
                Title = title,
                Display = false
            };
            newGallery.Order = await GalleryOrder();
            newGallery = GalleryAccordionIds(newGallery);

            _context.Entry(newGallery).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Helper method to auto assign a gallery order of last to new galleries
        /// </summary>
        /// <returns> list<galleries>.Count + 1 </galleries> </returns>
        private async Task<int> GalleryOrder()
        {
            var galleries = await GetGalleries();
            return galleries.Count + 1;
        }
        /// <summary>
        /// Helper method to normalize the input title and use it for class identification with bootstrap accordion.
        /// </summary>
        /// <param name="gallery"> new gallery object </param>
        /// <returns> updated gallery object </returns>
        private Gallery GalleryAccordionIds(Gallery gallery)
        {
            string str = (Regex.Replace(gallery.Title, @"\s+", String.Empty)).ToLower();

            gallery.AccordionId = str;
            gallery.CollapseId = $"{str}{gallery.Id}";
            gallery.AdminAccordionId = $"{str}admin";
            gallery.AdminCollapseId = $"{str}{gallery.Id}admin";

            return gallery;
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
                    AccordionId = y.AccordionId,
                    CollapseId = y.CollapseId,
                    AdminAccordionId = y.AdminAccordionId,
                    AdminCollapseId = y.AdminCollapseId,
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
                    AccordionId = y.AccordionId,
                    CollapseId = y.CollapseId,
                    AdminAccordionId = y.AdminAccordionId,
                    AdminCollapseId = y.AdminCollapseId,
                    GalleryImages = y.GalleryImages
                }).ToListAsync();
        }

        /// <summary>
        /// Update a gallery in the database
        /// </summary>
        /// <param name="gallery"> Gallery() object </param>
        public async Task UpdateGallery(Gallery gallery)
        {
            gallery = GalleryAccordionIds(gallery);
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
        /// Checks if a gallery title already exists
        /// </summary>
        /// <param name="title"> gallery title </param>
        /// <returns> true if exists </returns>
        public async Task<bool> CheckGalleryTitle(string title)
        {
            Gallery gallery = await _context.Galleries
                .Where(x => x.Title == title)
                .Select(y => new Gallery
                {
                    Id = y.Id,
                    Title = y.Title,
                    Display = y.Display,
                    Order = y.Order,
                    AccordionId = y.AccordionId,
                    CollapseId = y.CollapseId,
                    AdminAccordionId = y.AdminAccordionId,
                    AdminCollapseId = y.AdminCollapseId,
                    GalleryImages = y.GalleryImages
                }).FirstOrDefaultAsync();

            return gallery != null ? true : false;
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
