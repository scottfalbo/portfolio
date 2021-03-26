using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interfaces.Services
{
    public class AdminRepository : IAdmin
    {
        private readonly PortfolioDbContext _context;
        public AdminRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public Task CreateProject()
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProject(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Project>> GetProjects()
        {
            return await _context.Projects
                .Select(x => new Project
                {
                    Title = x.Title,
                    SourceURL = x.SourceURL,
                    Description = x.Description
                })
                .ToListAsync();
        }

        public Task<Project> UpdateProject(int Id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProject()
        {
            throw new NotImplementedException();
        }
    }
}
