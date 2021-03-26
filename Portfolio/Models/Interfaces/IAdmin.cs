using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interfaces
{
    public interface IAdmin
    {
        public Task CreateProject();
        public Task<Project> GetProject(int Id);
        public Task<List<Project>> GetProjects();
        public Task<Project> UpdateProject(int Id);
        public Task DeleteProject();
    }
}
