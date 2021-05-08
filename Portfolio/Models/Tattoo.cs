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
        public string ImageURL { get; set; }
        public string FileName { get; set; }
        public int Order { get; set; }
        public bool Display { get; set; }
    }
}
