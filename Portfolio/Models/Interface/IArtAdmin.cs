using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interface
{
    public interface IArtAdmin
    {
        /// <summary>
        /// Images CRUD
        /// </summary>
        public Task CreateImage(Image tattoo);
        public Task<Image> GetImage(int id);
        public Task<List<Image>> GetImages();
        public Task UpdateImage(Image tattoo);
        public Task DeleteImage(int id);
        public Task DeleteAll();

        /// <summary>
        /// gallery CRUD
        /// </summary>
        public Task CreateGallery(Gallery gallery);
        public Task<Gallery> GetGallery(int id);
        public Task<List<Gallery>> GetGalleries();
        public Task UpdateGallery(Gallery gallery);
        public Task DeleteGallery(int id);

        /// <summary>
        /// GalleryImage join table CRUD to add images to galleries.
        /// </summary>
        /// <param name="galleryId"> gallery id</param>
        /// <param name="imageId"> image id </param>
        public Task AddImageToGallery(int galleryId, int imageId);
        public Task RemoveImageFromGallery(int galleryId, int imageId);

        // Removes image from blob storage when it's removed from database
        public Task DeleteBlobImage(string fileName);
    }
}
