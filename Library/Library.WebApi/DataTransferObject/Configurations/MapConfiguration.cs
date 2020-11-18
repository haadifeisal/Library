﻿using AutoMapper;
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
            CreateMap<BookLibraryItemRequestDto, LibraryItem>();
            CreateMap<LibraryItem, BookLibraryItemResponseDto>();

            CreateMap<DvdLibraryItemRequestDto, LibraryItem>();
            CreateMap<LibraryItem, DvdLibraryItemResponseDto>();
        }
    }
}
