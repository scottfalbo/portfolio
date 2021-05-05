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
        public string SourceURL { get; set; }
        public string AltText { get; set; }
        public string Description { get; set; }
        public string RepoLink { get; set; }
        public string DeployedLink { get; set; }
        public int Order { get; set; }
    }
}
