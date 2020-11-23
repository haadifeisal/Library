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
        LibraryItem GetLibraryItem(int libraryItemId);
        LibraryItem CreateBookLibraryItem(BookLibraryItemRequestDto bookLibraryItemRequestDto);
        LibraryItem CreateDvdLibraryItem(DvdLibraryItemRequestDto dvdLibraryItemRequestDto);
        LibraryItem CreateAudioBookLibraryItem(AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto);
        LibraryItem CreateReferenceBookLibraryItem(ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto);
        bool BorrowLibraryItem(BorrowLibraryItemRequestDto borrowLibraryItemRequestDto);
        bool CheckInLibraryItem(int libraryItemId);
        LibraryItem UpdateBookLibraryItem(int libraryItemId, BookLibraryItemRequestDto bookLibraryItemRequestDto);
        LibraryItem UpdateDvdLibraryItem(int libraryItemId, DvdLibraryItemRequestDto dvdLibraryItemRequestDto);
        LibraryItem UpdateAudioBookLibraryItem(int libraryItemId, AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto);
        LibraryItem UpdateReferenceBookLibraryItem(int libraryItemId, ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto);
        bool DeleteLibraryItem(int libraryItemId);

    }
}
