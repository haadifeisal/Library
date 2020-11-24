using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject
{
    public class CategoryRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category Name must be minimum 2 charachters and maximum 50 characters long")]
        public string CategoryName { get; set; }
    }
}
