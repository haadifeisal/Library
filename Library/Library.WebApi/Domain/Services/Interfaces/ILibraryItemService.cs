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
        Task<ICollection<LibraryItem>> GetCollectionOfLibraryItems(bool sortByType = false);
        Task<LibraryItem> GetLibraryItem(int libraryItemId);
        Task<LibraryItem> CreateBookLibraryItem(BookLibraryItemRequestDto bookLibraryItemRequestDto);
        Task<LibraryItem> CreateDvdLibraryItem(DvdLibraryItemRequestDto dvdLibraryItemRequestDto);
        Task<LibraryItem> CreateAudioBookLibraryItem(AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto);
        Task<LibraryItem> CreateReferenceBookLibraryItem(ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto);
        Task<bool> BorrowLibraryItem(BorrowLibraryItemRequestDto borrowLibraryItemRequestDto);
        Task<bool> CheckInLibraryItem(int libraryItemId);
        Task<LibraryItem> UpdateBookLibraryItem(int libraryItemId, BookLibraryItemRequestDto bookLibraryItemRequestDto);
        Task<LibraryItem> UpdateDvdLibraryItem(int libraryItemId, DvdLibraryItemRequestDto dvdLibraryItemRequestDto);
        Task<LibraryItem> UpdateAudioBookLibraryItem(int libraryItemId, AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto);
        Task<LibraryItem> UpdateReferenceBookLibraryItem(int libraryItemId, ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto);
        Task<bool> DeleteLibraryItem(int libraryItemId);

    }
}
