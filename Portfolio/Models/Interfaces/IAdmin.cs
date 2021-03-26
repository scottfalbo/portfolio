using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models.Interfaces
{
    public interface IAdmin
    {
        public Task<List<Project>> GetProjects();
    }
}
