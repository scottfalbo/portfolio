using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TechSummary { get; set; }
        public string RepoLink { get; set; }
        public string DeployedLink { get; set; }
        public int Order { get; set; }
        public bool Display { get; set; }
        public string AccordionId { get; set; }
        public string CollapseId { get; set; }
        public string AdminAccordionId { get; set; }
        public string AdminCollapseId { get; set; }
        public List<ProjectTechnology> Technologies { get; set; }
        public List<ProjectImage> ProjectImages { get; set; }
    }
}
