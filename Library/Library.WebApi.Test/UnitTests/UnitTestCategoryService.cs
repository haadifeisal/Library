using Library.WebApi.DataTransferObject;
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
    public class UnitTestCategoryService : UnitTestBase
    {

        [TestMethod]
        public void Verify_AddCategory_Returns_True()
        {

            //Arrange
            var categoryService = new CategoryService(_context);

            //Act
            var addCategory = categoryService.AddCategory("Drama");

            //Assert
            Assert.IsTrue(addCategory);
        }

        [TestMethod]
        public void Verify_AddCategory_Returns_False_When_A_Dupplication_Of_Category_Name_Occurs()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            //Act
            var category = categoryService.AddCategory("Action");

            //Assert
            Assert.IsFalse(category);
        }

        [TestMethod]
        public void Verify_EditCategory_Returns_The_Edited_Category_Object()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            var categoryRequestDto = new CategoryRequestDto();
            categoryRequestDto.CategoryName = "Drama";

            //Act
            var category = categoryService.EditCategory(newCategory.Id, categoryRequestDto);

            //Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(newCategory.Id, category.Id);
        }

        [TestMethod]
        public void Verify_EditCategory_Returns_Null_When_Category_Id_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            var categoryRequestDto = new CategoryRequestDto();
            categoryRequestDto.CategoryName = "Drama";

            //Act
            var category = categoryService.EditCategory(4, categoryRequestDto);

            //Assert
            Assert.IsNull(category);
        }

        [TestMethod]
        public void Verify_EditCategory_Returns_Null_When_Theres_A_Duplicate_CategoryName()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            var categoryRequestDto = new CategoryRequestDto();
            categoryRequestDto.CategoryName = "Action";

            //Act
            var category = categoryService.EditCategory(newCategory.Id, categoryRequestDto);

            //Assert
            Assert.IsNull(category);
        }

        [TestMethod]
        public void Verify_DeleteCategory_Returns_True()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            /*var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.BorrowDate = DateTime.Now;
            newLibraryItem.Borrower = "Haadi";
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Pages = 10;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "Book";*/

            _context.Add(newCategory);
            //_context.Add(newLibraryItem);

            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            //Act
            var category = categoryService.DeleteCategory(newCategory.Id);

            //Assert
            Assert.IsTrue(category);
        }

        [TestMethod]
        public void Verify_DeleteCategory_Returns_False_When_CategoryId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            /*var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.BorrowDate = DateTime.Now;
            newLibraryItem.Borrower = "Haadi";
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Pages = 10;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "Book";*/

            _context.Add(newCategory);
            //_context.Add(newLibraryItem);

            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            //Act
            var category = categoryService.DeleteCategory(41);

            //Assert
            Assert.IsFalse(category);
        }

        [TestMethod]
        public void Verify_DeleteCategory_Returns_False_When_The_Category_Is_Referenced_By_A_LibraryItem()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);

            var newLibraryItem = new LibraryItem(); //Create the library item that will be used in this test.
            newLibraryItem.CategoryId = newCategory.Id;
            newLibraryItem.Author = "Paulo Coelho";
            newLibraryItem.BorrowDate = DateTime.Now;
            newLibraryItem.Borrower = "Haadi";
            newLibraryItem.IsBorrowable = false;
            newLibraryItem.Pages = 10;
            newLibraryItem.Title = "The Alchemist";
            newLibraryItem.Type = "Book";

            _context.Add(newLibraryItem);

            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            //Act
            var category = categoryService.DeleteCategory(newCategory.Id);

            //Assert
            Assert.IsFalse(category);
        }

    }
}
