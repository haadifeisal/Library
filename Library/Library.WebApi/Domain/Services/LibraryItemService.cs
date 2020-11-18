using AutoMapper;
using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services.Interfaces;
using Library.WebApi.Repository;
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

            _libraryContext.Add(libraryItem);
            _libraryContext.SaveChanges();

            return libraryItem;
        }

        public LibraryItem CreateDvdLibraryItem(DvdLibraryItemRequestDto dvdLibraryItemRequestDto)
        {
            var libraryItem = _mapper.Map<LibraryItem>(dvdLibraryItemRequestDto);

            _libraryContext.Add(libraryItem);
            _libraryContext.SaveChanges();

            return libraryItem;
        }

    }
}
