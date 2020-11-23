using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject
{
    public class ReferenceBookLibraryItemRequestDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? Pages { get; set; }
    }
}
