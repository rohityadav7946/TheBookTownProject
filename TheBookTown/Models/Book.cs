using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookTown.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Title { get; set; }
        [Required]
        
        public string Author { get; set; }
        [Required]
        public Genr? Genre { get; set; }
        public string PhotoPath { get; set; }

        public string BookPdfUrlPath { get; set; }



    }
}