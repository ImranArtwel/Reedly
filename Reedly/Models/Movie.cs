﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reedly.Models
{
    public class Movie
    {
        
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        public byte NumberInStock { get; set; }
        [Required]
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
    }
}