using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interface.Services
{
    public class ArtAdminRepository : IArtAdmin
    {
        public Task CreateTattoo()
        {
            return null;
        }

        public Task<Project> GetTattoo(int Id)
        {
            return null;
        }

        public Task<List<Project>> GetTattoos()
        {
            return null;
        }

        public Task<Project> UpdateTattoo(int id)
        {
            return null;
        }

        public Task DeleteTattoo(int id)
        {
            return null;
        }
        public Task DeleteAllTattoos()
        {
            return null;
        }



        public Task CreateDrawing()
        {
            return null;
        }

        public Task<Project> GetDrawing(int Id)
        {
            return null;
        }

        public Task<List<Project>> GetDrawings()
        {
            return null;
        }

        public Task<Project> UpdateDrawing(int Id)
        {
            return null;
        }

        public Task DeleteDrawing(int Id)
        {
            return null;
        }

        public Task DeleteAllDrawings()
        {
            return null;
        }
    }
}
