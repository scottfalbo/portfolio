using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interface
{
    public interface IArtAdmin
    {
        /// <summary>
        /// Tattoo portfolio CRUD
        /// </summary>
        public Task CreateImage(Image tattoo);
        public Task<Image> GetImage(int id);
        public Task<List<Image>> GetImages();
        public Task UpdateImage(Image tattoo);
        public Task DeleteImage(int id);
        public Task DeleteAllImages();

        public Task DeleteBlobImage(string fileName);
    }
}
