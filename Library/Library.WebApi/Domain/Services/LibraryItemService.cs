using AutoMapper;
using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services.Interfaces;
using Library.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Domain.Services
{
    public class LibraryItemService : ILibraryItemService
    {
        private readonly LibraryContext _libraryContext;
        private readonly IMapper _mapper;
        public LibraryItemService(LibraryContext libraryContext, IMapper mapper)
        {
            _libraryContext = libraryContext;
            _mapper = mapper;
        }

        public LibraryItem CreateBookLibraryItem(BookLibraryItemRequestDto bookLibraryItemRequestDto)
        {
            var libraryItem = _mapper.Map<LibraryItem>(bookLibraryItemRequestDto);

            if (CategoryExists(bookLibraryItemRequestDto.CategoryId))
            {
                return SaveToDb(libraryItem);
            }

            return null; // Category doesn't exist, therefore you cant create a LibraryItem.
        }

        public LibraryItem CreateDvdLibraryItem(DvdLibraryItemRequestDto dvdLibraryItemRequestDto)
        {
            var libraryItem = _mapper.Map<LibraryItem>(dvdLibraryItemRequestDto);

            if (CategoryExists(dvdLibraryItemRequestDto.CategoryId))
            {
                return SaveToDb(libraryItem);
            }

            return null; // Category doesn't exist, therefore you cant create a LibraryItem.
        }

        public LibraryItem CreateAudioBookLibraryItem(AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto)
        {
            var libraryItem = _mapper.Map<LibraryItem>(audioBookLibraryItemRequestDto);

            if (CategoryExists(audioBookLibraryItemRequestDto.CategoryId))
            {
                return SaveToDb(libraryItem);
            }

            return null; // Category doesn't exist, therefore you cant create a LibraryItem.
        }

        public LibraryItem CreateReferenceBookLibraryItem(ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto)
        {
            var libraryItem = _mapper.Map<LibraryItem>(referenceBookLibraryItemRequestDto);

            if (CategoryExists(referenceBookLibraryItemRequestDto.CategoryId))
            {
                return SaveToDb(libraryItem);
            }

            return null; // Category doesn't exist, therefore you cant create a LibraryItem.
        }

        public bool BorrowLibraryItem(BorrowLibraryItemRequestDto borrowLibraryItemRequestDto)
        {
            var libraryItem = _libraryContext.LibraryItems.FirstOrDefault(x => x.Id == borrowLibraryItemRequestDto.LibraryItemId
                 && x.IsBorrowable == true && x.Type != "reference book");

            if(libraryItem == null)
            {
                return false; //Either the libraryItem doesn't exist or the libraryItem is not borrowable.
            }

            libraryItem.Borrower = borrowLibraryItemRequestDto.Borrower;
            libraryItem.BorrowDate = DateTime.Now;

            if(_libraryContext.SaveChanges() == 1)
            {
                return true;
            }

            return false; // Better solution would be to log the exception to SeriLog.

        }

        public bool CheckInLibraryItem(int libraryItemId)
        {
            var libraryItem = _libraryContext.LibraryItems.FirstOrDefault(x => x.Id == libraryItemId && x.IsBorrowable == false
                && x.Type != "reference book");

            if(libraryItem == null)
            {
                return false; // Either the libraryItem doesn't exist or the libraryItem is already borrowable, which means you cant
                // check in an item that's already borrowable.
            }

            libraryItem.IsBorrowable = true;
            libraryItem.Borrower = null;
            libraryItem.BorrowDate = null;

            if(_libraryContext.SaveChanges() == 1)
            {
                return true;
            }

            return false; // Better solution would be to log the exception to SeriLog.
        }

        public LibraryItem EditBookLibraryItem(int libraryItemId, BookLibraryItemRequestDto bookLibraryItemRequestDto)
        {
            var libraryItem = _libraryContext.LibraryItems.FirstOrDefault(x => x.Id == libraryItemId);

            if(libraryItem == null)
            {
                return null; // LibraryItem doesn't exist.
            }

            if(bookLibraryItemRequestDto.Type == "book" || bookLibraryItemRequestDto.Type == "dvd"
                || bookLibraryItemRequestDto.Type == "audio book" || bookLibraryItemRequestDto.Type == "reference book")
            {
                libraryItem.Type = bookLibraryItemRequestDto.Type;
            }
            libraryItem.CategoryId = CategoryExists(bookLibraryItemRequestDto.CategoryId) ? bookLibraryItemRequestDto.CategoryId : libraryItem.CategoryId;
            libraryItem.Author = string.IsNullOrEmpty(bookLibraryItemRequestDto.Author) ? libraryItem.Author : bookLibraryItemRequestDto.Author; ;
            libraryItem.Pages = bookLibraryItemRequestDto.Pages != null ? bookLibraryItemRequestDto.Pages : null;
            libraryItem.IsBorrowable = bookLibraryItemRequestDto.IsBorrowable ? true : false;
            libraryItem.Title = string.IsNullOrEmpty(bookLibraryItemRequestDto.Title) ? libraryItem.Title : bookLibraryItemRequestDto.Title;

            if(_libraryContext.SaveChanges() == 1)
            {
                return libraryItem;
            }

            return null; // Better solution would be to log the exception to SeriLog.
        }

        private bool CategoryExists(int id)
        {
            var category = _libraryContext.Categories.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if(category != null)
            {
                return true;
            }

            return false;
        }

        private LibraryItem SaveToDb(LibraryItem libraryItem)
        {
            _libraryContext.Add(libraryItem);

            if (_libraryContext.SaveChanges() == 1)
            {
                return libraryItem;
            }

            return null; // Better solution would be to log the exception to SeriLog.
        }

    }
}
