using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ProjectImage
    {
        public int ProjectId { get; set; }
        public int ImageId { get; set; }
        public Project Project { get; set; }
        public Image Image { get; set; }
    }
}
