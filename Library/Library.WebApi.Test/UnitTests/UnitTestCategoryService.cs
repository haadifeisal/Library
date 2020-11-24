using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services;
using Library.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.WebApi.Test.UnitTests
{
    [TestClass]
    public class UnitTestCategoryService : UnitTestBase
    {
        [TestMethod]
        public async Task Verify_GetCollectionOfCategories_Returns_A_Collection_Of_Categories()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";
            _context.Add(newCategory);

            var newCategory2 = new Category(); //Create the category object.
            newCategory2.CategoryName = "Drama";
            _context.Add(newCategory2);

            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            //Act
            var categoryCollection = await categoryService.GetCollectionOfCategories();

            //Assert
            Assert.IsTrue(categoryCollection.Count > 0);
        }


        [TestMethod]
        public async Task Verify_AddCategory_Returns_True()
        {

            //Arrange
            var categoryRequestDto = new CategoryRequestDto
            {
                CategoryName = "Drama"
            };

            var categoryService = new CategoryService(_context);

            //Act
            var addCategory = await categoryService.CreateCategory(categoryRequestDto);

            //Assert
            Assert.IsTrue(addCategory);
        }

        [TestMethod]
        public async Task Verify_AddCategory_Returns_False_When_A_Dupplication_Of_Category_Name_Occurs()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var categoryRequestDto = new CategoryRequestDto
            {
                CategoryName = "Action"
            };

            var categoryService = new CategoryService(_context);

            //Act
            var category = await categoryService.CreateCategory(categoryRequestDto);

            //Assert
            Assert.IsFalse(category);
        }

        [TestMethod]
        public async Task Verify_EditCategory_Returns_The_Edited_Category_Object()
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
            var category = await categoryService.EditCategory(newCategory.Id, categoryRequestDto);

            //Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(newCategory.Id, category.Id);
        }

        [TestMethod]
        public async Task Verify_EditCategory_Returns_Null_When_Category_Id_Is_Not_Found()
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
            var category = await categoryService.EditCategory(4, categoryRequestDto);

            //Assert
            Assert.IsNull(category);
        }

        [TestMethod]
        public async Task Verify_EditCategory_Returns_Null_When_Theres_A_Duplicate_CategoryName()
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
            var category = await categoryService.EditCategory(newCategory.Id, categoryRequestDto);

            //Assert
            Assert.IsNull(category);
        }

        [TestMethod]
        public async Task Verify_DeleteCategory_Returns_True()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            //Act
            var category = await categoryService.DeleteCategory(newCategory.Id);

            //Assert
            Assert.IsTrue(category);
        }

        [TestMethod]
        public async Task Verify_DeleteCategory_Returns_False_When_CategoryId_Is_Not_Found()
        {

            //Arrange
            var newCategory = new Category(); //Create the category object.
            newCategory.CategoryName = "Action";

            _context.Add(newCategory);
            _context.SaveChanges();

            var categoryService = new CategoryService(_context);

            //Act
            var category = await categoryService.DeleteCategory(41);

            //Assert
            Assert.IsFalse(category);
        }

        [TestMethod]
        public async Task Verify_DeleteCategory_Returns_False_When_The_Category_Is_Referenced_By_A_LibraryItem()
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
            var category = await categoryService.DeleteCategory(newCategory.Id);

            //Assert
            Assert.IsFalse(category);
        }

    }
}
