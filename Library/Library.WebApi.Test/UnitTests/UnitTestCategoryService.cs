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
        
        [TestMethod]
        public void Verify_AddCategory_Returns_True()
        {
            var dbName = Guid.NewGuid().ToString();
            DbContextOptions<LibraryContext> options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            //Arrange
            //var categoryService = new CategoryService(options);

            //Act

            //Assert
        }

    }
}
