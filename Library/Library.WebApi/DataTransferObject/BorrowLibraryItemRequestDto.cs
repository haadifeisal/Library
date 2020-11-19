﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject
{
    public class BorrowLibraryItemRequestDto
    {
        public int LibraryItemId { get; set; }
        public string Borrower { get; set; }
        public DateTime BorrowDate { get; set; }
    }
}
