using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services;
using Library.WebApi.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.WebApi.Test.UnitTests
{
    [TestClass]
    public class UnitTestLibraryItemService : UnitTestBase
    {

        [TestMethod]
        public void Verify_GetCollectionOfLibraryItems_Returns_A_Collection_LibraryItems_That_Are_Sorted_By_Category()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            var newCategory2 = new Category(); //Create the category object.
            newCategory2.CategoryName = "Action";

            var newCategory3 = new Category(); //Create the category object.
            newCategory3.CategoryName = "Horror";

            _context.Add(newCategory);
            _context.Add(newCategory2);
            _context.Add(newCategory3);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);

            var newLibraryItem2 = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem2.CategoryId = newCategory2.Id;
            newLibraryItem2.Author = "George Lucas";
            newLibraryItem2.IsBorrowable = false;
            newLibraryItem2.Pages = 51;
            newLibraryItem2.Title = "Star Wars";
            newLibraryItem2.Type = "reference book";

            _context.LibraryItems.Add(newLibraryItem2);

            var newLibraryItem3 = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem3.CategoryId = newCategory3.Id;
            newLibraryItem3.Author = "Wes Craven";
            newLibraryItem3.IsBorrowable = true;
            newLibraryItem3.RunTimeMinutes = 07;
            newLibraryItem3.Title = "Nightmare on elmstreet";
            newLibraryItem3.Type = "dvd";

            _context.LibraryItems.Add(newLibraryItem3);
            _context.SaveChanges();

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItemCollection = libraryItemService.GetCollectionOfLibraryItems();

            //Assert
            Assert.IsNotNull(libraryItemCollection);
            Assert.AreEqual(libraryItemCollection.Count, 3);
            Assert.AreEqual(libraryItemCollection.First().Category.CategoryName, "Action");
            Assert.AreEqual(libraryItemCollection.Last().Category.CategoryName, "Horror");
        }

        [TestMethod]
        public void Verify_GetCollectionOfLibraryItems_Returns_A_Collection_LibraryItems_That_Are_Sorted_By_Type()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            var newCategory2 = new Category(); //Create the category object.
            newCategory2.CategoryName = "Action";

            var newCategory3 = new Category(); //Create the category object.
            newCategory3.CategoryName = "Horror";

            _context.Add(newCategory);
            _context.Add(newCategory2);
            _context.Add(newCategory3);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);

            var newLibraryItem2 = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem2.CategoryId = newCategory2.Id;
            newLibraryItem2.Author = "George Lucas";
            newLibraryItem2.IsBorrowable = false;
            newLibraryItem2.Pages = 51;
            newLibraryItem2.Title = "Star Wars";
            newLibraryItem2.Type = "reference book";

            _context.LibraryItems.Add(newLibraryItem2);

            var newLibraryItem3 = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem3.CategoryId = newCategory3.Id;
            newLibraryItem3.Author = "Wes Craven";
            newLibraryItem3.IsBorrowable = true;
            newLibraryItem3.RunTimeMinutes = 07;
            newLibraryItem3.Title = "Nightmare on elmstreet";
            newLibraryItem3.Type = "dvd";

            _context.LibraryItems.Add(newLibraryItem3);
            _context.SaveChanges();

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItemCollection = libraryItemService.GetCollectionOfLibraryItems(true);

            //Assert
            Assert.IsNotNull(libraryItemCollection);
            Assert.AreEqual(libraryItemCollection.Count, 3);
            Assert.AreEqual(libraryItemCollection.First().Type, "book");
            Assert.AreEqual(libraryItemCollection.Last().Type, "reference book");
        }

        [TestMethod]
        public void Verify_GetCollectionOfLibraryItems_Returns_A_Collection_LibraryItems_With_Correct_Acronym_On_Title()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist One";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItemCollection = libraryItemService.GetCollectionOfLibraryItems();

            //Assert
            Assert.AreNotEqual(libraryItemCollection.Count, 0);
            Assert.AreEqual(libraryItemCollection.First().Title, "The Alchemist One (TAO)");
        }

        [TestMethod]
        public void Verify_CreateBookLibraryItem_Returns_The_Created_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var newLibraryItem = new BookLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateBookLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNotNull(libraryItem);
            Assert.AreEqual(newLibraryItem.Author, libraryItem.Author);
        }

        [TestMethod]
        public void Verify_CreateBookLibraryItem_Returns_Null_When_Category_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var newLibraryItem = new BookLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = 9;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateBookLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNull(libraryItem);
        }

        [TestMethod]
        public void Verify_CreateDvdLibraryItem_Returns_The_Created_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var newLibraryItem = new DvdLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 74;
            newLibraryItem.Title = "Bad Boys";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateDvdLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNotNull(libraryItem);
            Assert.AreEqual(newLibraryItem.Title, libraryItem.Title);
        }

        [TestMethod]
        public void Verify_CreateDvdLibraryItem_Returns_Null_When_CategoryId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var newLibraryItem = new DvdLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = 7;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 74;
            newLibraryItem.Title = "Bad Boys";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateDvdLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNull(libraryItem);
        }

        [TestMethod]
        public void Verify_CreateAudioBookLibraryItem_Returns_The_Created_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Drama";

            _context.Add(newCategory);
            _context.SaveChanges();

            var newLibraryItem = new AudioBookLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 135;
            newLibraryItem.Title = "Man On A Mission";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateAudioBookLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNotNull(libraryItem);
            Assert.AreEqual(newLibraryItem.Title, libraryItem.Title);
        }

        [TestMethod]
        public void Verify_CreateAudioBookLibraryItem_Returns_Null_When_CategoryId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Drama";

            _context.Add(newCategory);
            _context.SaveChanges();

            var newLibraryItem = new AudioBookLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = 4;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 135;
            newLibraryItem.Title = "Man On A Mission";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateAudioBookLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNull(libraryItem);
        }

        [TestMethod]
        public void Verify_CreateReferenceBookLibraryItem_Returns_The_Created_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Drama";

            _context.Add(newCategory);
            _context.SaveChanges();

            var newLibraryItem = new ReferenceBookLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "James Bond";
            newLibraryItem.Pages = 68;
            newLibraryItem.Title = "GreenField";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateReferenceBookLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNotNull(libraryItem);
            Assert.AreEqual(newLibraryItem.Title, libraryItem.Title);
        }

        [TestMethod]
        public void Verify_CreateReferenceBookLibraryItem_Returns_Null_When_CategoryId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Drama";

            _context.Add(newCategory);
            _context.SaveChanges();

            var newLibraryItem = new ReferenceBookLibraryItemRequestDto(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = 18;
            newLibraryItem.Author = "James Bond";
            newLibraryItem.Pages = 68;
            newLibraryItem.Title = "GreenField";

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CreateReferenceBookLibraryItem(newLibraryItem);

            //Assert
            Assert.IsNull(libraryItem);
        }

        [TestMethod]
        public void Verify_BorrowLibraryItem_Returns_True()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var borrowLibraryItem = new BorrowLibraryItemRequestDto();
            borrowLibraryItem.LibraryItemId = newLibraryItem.Id;
            borrowLibraryItem.Borrower = "Haadi";
            borrowLibraryItem.BorrowDate = DateTime.Now;

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.BorrowLibraryItem(borrowLibraryItem);

            //Assert
            Assert.IsTrue(libraryItem);
        }

        [TestMethod]
        public void Verify_BorrowLibraryItem_Returns_False_When_LibraryItemId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var borrowLibraryItem = new BorrowLibraryItemRequestDto();
            borrowLibraryItem.LibraryItemId = 8;
            borrowLibraryItem.Borrower = "Haadi";
            borrowLibraryItem.BorrowDate = DateTime.Now;

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.BorrowLibraryItem(borrowLibraryItem);

            //Assert
            Assert.IsFalse(libraryItem);
        }

        [TestMethod]
        public void Verify_BorrowLibraryItem_Returns_False_When_LibraryItem_Is_Borrowed_By_Another_Person()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Borrower = "Spiderman";
            newLibraryItem.BorrowDate = DateTime.Now;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var borrowLibraryItem = new BorrowLibraryItemRequestDto();
            borrowLibraryItem.LibraryItemId = newLibraryItem.Id;
            borrowLibraryItem.Borrower = "Haadi";
            borrowLibraryItem.BorrowDate = DateTime.Now;

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.BorrowLibraryItem(borrowLibraryItem);

            //Assert
            Assert.IsFalse(libraryItem);
        }

        [TestMethod]
        public void Verify_BorrowLibraryItem_Returns_False_When_User_Is_Trying_To_Borrow_A_ReferenceBook()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "reference book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var borrowLibraryItem = new BorrowLibraryItemRequestDto();
            borrowLibraryItem.LibraryItemId = newLibraryItem.Id;
            borrowLibraryItem.Borrower = "Haadi";
            borrowLibraryItem.BorrowDate = DateTime.Now;

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.BorrowLibraryItem(borrowLibraryItem);

            //Assert
            Assert.IsFalse(libraryItem);
        }

        [TestMethod]
        public void Verify_CheckInLibraryItem_Returns_True()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Borrower = "Haadi";
            newLibraryItem.BorrowDate = DateTime.Now;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CheckInLibraryItem(newLibraryItem.Id);

            //Assert
            Assert.IsTrue(libraryItem);
        }

        [TestMethod]
        public void Verify_CheckInLibraryItem_Returns_False_When_The_LibraryItem_Is_A_ReferenceBook()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "reference book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var libraryItem = libraryItemService.CheckInLibraryItem(newLibraryItem.Id);

            //Assert
            Assert.IsFalse(libraryItem);
        }

        [TestMethod]
        public void Verify_EditBookLibraryItem_Returns_The_Edited_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editBook = new BookLibraryItemRequestDto
            {
                CategoryId = newLibraryItem.CategoryId,
                Author = "Paulo Coelho",
                IsBorrowable = true,
                Pages = 100,
                Title = "True Story"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedBook = libraryItemService.EditBookLibraryItem(newLibraryItem.Id, editBook);

            //Assert
            Assert.IsNotNull(editedBook);
            Assert.AreEqual(editedBook.Title, "True Story");
        }

        [TestMethod]
        public void Verify_EditBookLibraryItem_Returns_Null_When_LibraryItem_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editBook = new BookLibraryItemRequestDto
            {
                CategoryId = newLibraryItem.Id,
                Author = "Paulo Coelho",
                IsBorrowable = true,
                Pages = 100,
                Title = "True Story"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedBook = libraryItemService.EditBookLibraryItem(9, editBook);

            //Assert
            Assert.IsNull(editedBook);
        }

        [TestMethod]
        public void Verify_EditBookLibraryItem_Sets_Previous_CategoryId_When_New_CategoryId_Is_Not_Found()
        { // If the BookLibraryItemRequestDto has an invalid categoryId, the edited library Item will set
            // the previous categoryId.

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Pages = 74;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editBook = new BookLibraryItemRequestDto
            {
                CategoryId = 8,
                Author = "Paulo Coelho",
                IsBorrowable = true,
                Pages = 100,
                Title = "True Story"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedBook = libraryItemService.EditBookLibraryItem(newLibraryItem.Id, editBook);

            //Assert
            Assert.IsNotNull(editedBook);
            Assert.AreEqual(editedBook.CategoryId, newLibraryItem.CategoryId);
        }

        [TestMethod]
        public void Verify_EditDvdLibraryItem_Returns_The_Edited_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 90;
            newLibraryItem.Title = "Batman";
            newLibraryItem.Type = "dvd";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editDvd = new DvdLibraryItemRequestDto
            {
                CategoryId = newLibraryItem.CategoryId,
                IsBorrowable = true,
                RunTimeMinutes = 140,
                Title = "Spiderman 3"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedDvd = libraryItemService.EditDvdLibraryItem(newLibraryItem.Id, editDvd);

            //Assert
            Assert.IsNotNull(editedDvd);
            Assert.AreEqual(editedDvd.Title, "Spiderman 3");
        }

        [TestMethod]
        public void Verify_EditDvdLibraryItem_Returns_Null_LibraryItemId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 90;
            newLibraryItem.Title = "Batman";
            newLibraryItem.Type = "dvd";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editDvd = new DvdLibraryItemRequestDto
            {
                CategoryId = newLibraryItem.CategoryId,
                IsBorrowable = true,
                RunTimeMinutes = 140,
                Title = "Spiderman 3"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedDvd = libraryItemService.EditDvdLibraryItem(57, editDvd);

            //Assert
            Assert.IsNull(editedDvd);
        }

        [TestMethod]
        public void Verify_EditAudioBookLibraryItem_Returns_The_Edited_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Drama";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 84;
            newLibraryItem.Title = "Chasing Dreams";
            newLibraryItem.Type = "audio book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editAudioBook = new AudioBookLibraryItemRequestDto
            {
                CategoryId = newLibraryItem.CategoryId,
                IsBorrowable = true,
                RunTimeMinutes = 45,
                Title = "Chasing dreams"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedAudioBook = libraryItemService.EditAudioBookLibraryItem(newLibraryItem.Id, editAudioBook);

            //Assert
            Assert.IsNotNull(editedAudioBook);
            Assert.AreEqual(editedAudioBook.RunTimeMinutes, 45);
        }

        [TestMethod]
        public void Verify_EditAudioBookLibraryItem_Returns_Null_When_LibraryItemId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Drama";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 84;
            newLibraryItem.Title = "Chasing Dreams";
            newLibraryItem.Type = "audio book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editAudioBook = new AudioBookLibraryItemRequestDto
            {
                CategoryId = newLibraryItem.CategoryId,
                IsBorrowable = true,
                RunTimeMinutes = 45,
                Title = "Chasing dreams"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedAudioBook = libraryItemService.EditAudioBookLibraryItem(7, editAudioBook);

            //Assert
            Assert.IsNull(editedAudioBook);
        }

        [TestMethod]
        public void Verify_EditReferenceBookLibraryItem_Returns_The_Edited_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Drama";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Pages = 40;
            newLibraryItem.Author = "James Bod";
            newLibraryItem.Title = "007";
            newLibraryItem.Type = "reference book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editReferenceBook = new ReferenceBookLibraryItemRequestDto
            {
                CategoryId = newLibraryItem.CategoryId,
                Author = "James Bond",
                Pages = 73,
                Title = "Mr 007"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedReferenceBook = libraryItemService.EditReferenceBookLibraryItem(newLibraryItem.Id, editReferenceBook);

            //Assert
            Assert.IsNotNull(editedReferenceBook);
            Assert.AreEqual(editedReferenceBook.Title, "Mr 007");
        }

        [TestMethod]
        public void Verify_EditReferenceBookLibraryItem_Returns_Null_When_LibraryItemId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Drama";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Pages = 40;
            newLibraryItem.Author = "James Bod";
            newLibraryItem.Title = "007";
            newLibraryItem.Type = "reference book";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var editReferenceBook = new ReferenceBookLibraryItemRequestDto
            {
                CategoryId = newLibraryItem.CategoryId,
                Author = "James Bond",
                Pages = 73,
                Title = "Mr 007"
            };


            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var editedReferenceBook = libraryItemService.EditReferenceBookLibraryItem(11, editReferenceBook);

            //Assert
            Assert.IsNull(editedReferenceBook);
        }

        [TestMethod]
        public void Verify_DeletedLibraryItem_Returns_True()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.RunTimeMinutes = 90;
            newLibraryItem.Title = "Batman";
            newLibraryItem.Type = "dvd";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var deletedLibraryItem = libraryItemService.DeleteLibraryItem(newLibraryItem.Id);

            //Assert
            Assert.IsTrue(deletedLibraryItem);
        }

        [TestMethod]
        public void Verify_DeletedLibraryItem_Returns_False_When_The_LibraryItem_Is_Currently_Borrowed_By_Someone()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Fiction";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.IsBorrowable = true;
            newLibraryItem.Borrower = "Haadi";
            newLibraryItem.BorrowDate = DateTime.Now;
            newLibraryItem.RunTimeMinutes = 90;
            newLibraryItem.Title = "Batman";
            newLibraryItem.Type = "dvd";

            _context.LibraryItems.Add(newLibraryItem);
            _context.SaveChanges();

            var libraryItemService = new LibraryItemService(_context, _mapper);

            //Act
            var deletedLibraryItem = libraryItemService.DeleteLibraryItem(newLibraryItem.Id);

            //Assert
            Assert.IsFalse(deletedLibraryItem);
        }

    }

}
