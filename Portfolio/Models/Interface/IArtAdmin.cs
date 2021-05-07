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
        public Task CreateTattoo();
        public Task<Project> GetTattoo(int id);
        public Task<List<Project>> GetTattoos();
        public Task<Project> UpdateTattoo(int id);
        public Task DeleteTattoo(int id);
        public Task DeleteAllTattoos();

        /// <summary>
        /// Drawing portfolio CRUD
        /// </summary>
        public Task CreateDrawing();
        public Task<Project> GetDrawing(int id);
        public Task<List<Project>> GetDrawings();
        public Task<Project> UpdateDrawing(int id);
        public Task DeleteDrawing(int id);
        public Task DeleteAllDrawings();

        public Task DeleteBlobImage(string fileName);
    }
}
