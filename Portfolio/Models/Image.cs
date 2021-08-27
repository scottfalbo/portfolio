using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AltText { get; set; }
        public string ImageURL { get; set; }
        public string FileName { get; set; }
        public string ThumbURL { get; set; }
        public string ThumbFileName { get; set; }
        public int Order { get; set; }
        public List<GalleryImage> GalleryImage { get; set; }
        public List<ProjectImage> ProjectImage { get; set; }
        
    }
}
