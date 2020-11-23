using AutoMapper;
using Library.WebApi.DataTransferObject.Configurations;
using Library.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebApi.Test.UnitTests
{
    public class UnitTestBase
    {
        public readonly LibraryContext _context;
        public readonly IMapper _mapper;

        public UnitTestBase()
        {

            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            _context = new LibraryContext(options);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapConfiguration());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _mapper = mapper;

        }


    }
}
