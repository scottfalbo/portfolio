using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interface
{
    public interface IAdmin
    {
        /// <summary>
        /// Projects portfolio CRUD
        /// </summary>
        public Task CreateProject(Project project);
        public Task<Project> GetProject(int id);
        public Task<List<Project>> GetProjects();
        public Task UpdateProject(Project project);
        public Task DeleteProject(int id);
        public Task DeleteBlobImage(string fileName);
        public Task<HomePage> GetHomePage();
    }
}
