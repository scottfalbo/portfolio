﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SourceURL { get; set; }
        public string AltText { get; set; }
        [Required]
        public string Description { get; set; }
        public string RepoLink { get; set; }
        public string DeployedLink { get; set; }
    }
}
