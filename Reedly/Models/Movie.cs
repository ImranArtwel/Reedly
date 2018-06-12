using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Reedly.Models
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // to avoid identity_column set off error
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name ="Number in Stock")]
        public byte NumberInStock { get; set; }
        
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
    }
}