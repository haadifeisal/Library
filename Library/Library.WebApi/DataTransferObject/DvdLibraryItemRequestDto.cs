using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject
{
    public class DvdLibraryItemRequestDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public int? RunTimeMinutes { get; set; }
        public bool IsBorrowable { get; set; }
        public string Type { get; set; }
    }
}
