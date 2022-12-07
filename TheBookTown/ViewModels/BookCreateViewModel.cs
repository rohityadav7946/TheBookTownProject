using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookTown.Models
{
    public class BookCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }
        [Required]
       
        public string Author { get; set; }
        [Required]
        public Genr? Genre { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
}
