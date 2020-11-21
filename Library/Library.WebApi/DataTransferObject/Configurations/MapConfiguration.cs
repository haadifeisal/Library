using AutoMapper;
using Library.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.DataTransferObject.Configurations
{
    public class MapConfiguration : Profile
    {
        public MapConfiguration()
        {
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<LibraryItem, LibraryItemResponseDto>()
                .ForMember(x => x.Category, y => y.MapFrom(z => z.Category));

            CreateMap<BookLibraryItemRequestDto, LibraryItem>();
            CreateMap<LibraryItem, BookLibraryItemResponseDto>();

            CreateMap<DvdLibraryItemRequestDto, LibraryItem>();
            CreateMap<LibraryItem, DvdLibraryItemResponseDto>();

            CreateMap<AudioBookLibraryItemRequestDto, LibraryItem>();
            CreateMap<LibraryItem, AudioBookLibraryItemResponseDto>();

            CreateMap<ReferenceBookLibraryItemRequestDto, LibraryItem>();
            CreateMap<LibraryItem, ReferenceBookLibraryItemResponseDto>();

        }
    }
}
