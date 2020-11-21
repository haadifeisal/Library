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
        ICollection<LibraryItem> GetCollectionOfLibraryItems(bool sortByType = false);
        LibraryItem CreateBookLibraryItem(BookLibraryItemRequestDto bookLibraryItemRequestDto);
        LibraryItem CreateDvdLibraryItem(DvdLibraryItemRequestDto dvdLibraryItemRequestDto);
        LibraryItem CreateAudioBookLibraryItem(AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto);
        LibraryItem CreateReferenceBookLibraryItem(ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto);
        bool BorrowLibraryItem(BorrowLibraryItemRequestDto borrowLibraryItemRequestDto);
        bool CheckInLibraryItem(int libraryItemId);
        LibraryItem EditBookLibraryItem(int libraryItemId, BookLibraryItemRequestDto bookLibraryItemRequestDto);
        LibraryItem EditDvdLibraryItem(int libraryItemId, DvdLibraryItemRequestDto dvdLibraryItemRequestDto);
        LibraryItem EditAudioBookLibraryItem(int libraryItemId, AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto);
        LibraryItem EditReferenceBookLibraryItem(int libraryItemId, ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto);
        bool DeleteLibraryItem(int libraryItemId);

    }
}
