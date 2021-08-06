using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Display { get; set; }
        public List<GalleryImage> GalleryImages { get; set; }
    }
}
