using Library.WebApi.Domain.Services;
using Library.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebApi.Test.UnitTests
{
    [TestClass]
    public class UnitTestCategoryService
    {
        private LibraryContext _context;

        [TestMethod]
        public void Verify_AddCategory_Returns_True()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            _context = new LibraryContext(options);

            //Arrange
            var categoryService = new CategoryService(_context);

            //Act
            var addCategory = categoryService.AddCategory("Drama");

            //Assert
            Assert.IsTrue(addCategory);
        }

    }
}
