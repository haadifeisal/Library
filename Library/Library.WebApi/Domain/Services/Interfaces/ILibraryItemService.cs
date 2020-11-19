using Library.WebApi.DataTransferObject;
using Library.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Domain.Services.Interfaces
{
    public interface ILibraryItemService
    {
        LibraryItem CreateBookLibraryItem(BookLibraryItemRequestDto bookLibraryItemRequestDto);
        LibraryItem CreateDvdLibraryItem(DvdLibraryItemRequestDto dvdLibraryItemRequestDto);
        LibraryItem CreateAudioBookLibraryItem(AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto);
    }
}
