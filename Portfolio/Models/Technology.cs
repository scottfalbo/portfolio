using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Technology
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string LogoUrl { get; set; }
        public List<ProjectTechnology> ProjectTechnology { get; set; }
        public List<HomePageTechnology> HomePageTechnology { get; set; }
    }
}
