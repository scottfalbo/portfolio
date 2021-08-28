using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class HomePageTechnology
    {
        public int HomePageId { get; set; }
        public int TechnologyId { get; set; }
        public bool Display { get; set; }
        public HomePage HomePage { get; set; }
        public Technology Technology { get; set; }
    }
}
