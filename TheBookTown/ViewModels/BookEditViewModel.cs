using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookTown.Models
{
    public class BookEditViewModel : BookCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingPhotoPath  { get; set; }

        public string ExistingPdfUrl { get; set; }

    }
}
