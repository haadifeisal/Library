using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject
{
    public class BookLibraryItemRequestDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? Pages { get; set; }
        public bool IsBorrowable { get; set; }
    }
}
