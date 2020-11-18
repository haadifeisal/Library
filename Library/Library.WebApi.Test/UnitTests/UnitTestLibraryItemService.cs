using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services;
using Library.WebApi.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebApi.Test.UnitTests
{
    [TestClass]
    public class UnitTestLibraryItemService : UnitTestBase
    {
        [TestMethod]
        public void Verify_CreateBookLibraryItem_Returns_A_LibraryItem_Object()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);

            var newLibraryItem = new BookLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "Book";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateBookLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNotNull(libraryItem);
            Assert.AreEqual(newLibraryItem.Author, libraryItem.Author);
        }
    }
}
