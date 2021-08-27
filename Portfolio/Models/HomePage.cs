using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class HomePage
    {
        public int Id { get; set; }
        public string Page { get; set; }
        public string Title { get; set; }
        public string Intro { get; set; }
        public string Selfie { get; set; }
        public string FileName { get; set; }
        public List<HomePageTechnology> Technologies { get; set; }
    }
}
