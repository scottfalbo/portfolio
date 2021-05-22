using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// HomePage CRUDs
        /// </summary>
        public Task<HomePage> GetHomePage(string page);
        public Task<List<HomePage>> GetHomePages();
        public Task UpdateHomePage(HomePage homepage);

        /// <summary>
        /// Instagram API methods
        /// </summary>
        public Task GetInstagramFeed();
        public void RefreshAccessToken();
    }
}
