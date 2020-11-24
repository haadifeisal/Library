using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject
{
    public class EmployeeUpdateRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "FirstName must be minimum 2 charachters and maximum 50 characters long")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "LastName must be minimum 2 charachters and maximum 50 characters long")]
        public string LastName { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Rank must be between 1 and 10")]
        public int Rank { get; set; }
        public int? ManagerId { get; set; }
    }
}
