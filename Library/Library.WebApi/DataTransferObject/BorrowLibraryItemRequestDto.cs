using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject
{
    public class BorrowLibraryItemRequestDto
    {
        [Required]
        public int LibraryItemId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Borrower name must be minimum 2 charachters and maximum 100 characters long")]
        public string Borrower { get; set; }
    }
}
