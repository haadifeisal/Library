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

        public UnitTestBase()
        {

            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            _context = new LibraryContext(options);

        }


    }
}
