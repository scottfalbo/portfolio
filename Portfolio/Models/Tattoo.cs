using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Tattoo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string ImageURL { get; set; }
        public int Order { get; set; }
    }
}
