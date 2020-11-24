using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject
{
    public class BookLibraryItemRequestDto
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Title must be minimum 2 charachters and maximum 50 characters long")]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Author must be minimum 2 charachters and maximum 100 characters long")]
        public string Author { get; set; }
        [Required]
        public int? Pages { get; set; }
        [Required]
        public bool IsBorrowable { get; set; }
    }
}
