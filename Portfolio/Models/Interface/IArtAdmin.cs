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
        public Task CreateTattoo(Tattoo tattoo);
        public Task<Tattoo> GetTattoo(int id);
        public Task<List<Tattoo>> GetTattoos();
        public Task UpdateTattoo(Tattoo tattoo);
        public Task DeleteTattoo(int id);
        public Task DeleteAllTattoos();

        /// <summary>
        /// Drawing portfolio CRUD
        /// </summary>
        public Task CreateDrawing(Drawing drawing);
        public Task<Drawing> GetDrawing(int id);
        public Task<List<Drawing>> GetDrawings();
        public Task UpdateDrawing(Drawing drawing);
        public Task DeleteDrawing(int id);
        public Task DeleteAllDrawings();

        public Task DeleteBlobImage(string fileName);
    }
}
