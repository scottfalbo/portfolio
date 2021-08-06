using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class GalleryImage
    {
        public int GalleryId { get; set; }
        public int ImageId { get; set; }
        public Gallery Gallery { get; set; }
        public Image Image { get; set; }
    }
}
