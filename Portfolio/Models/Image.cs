﻿using System;
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
        public string Type { get; set; }
        public string ImageURL { get; set; }
        public string FileName { get; set; }
        public int Order { get; set; }
        
    }
}