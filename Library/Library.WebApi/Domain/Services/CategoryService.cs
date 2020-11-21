using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services.Interfaces;
using Library.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryContext _libraryContext;
        public CategoryService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public ICollection<Category> GetCollectionOfCategories()
        {
            var categoryCollection = _libraryContext.Categories.AsNoTracking().ToList();

            return categoryCollection;
        }

        public bool AddCategory(CategoryRequestDto categoryRequestDto)
        {

            if(DuplicateCategory(categoryRequestDto.CategoryName))
            {
                return false;
            }

            var newCategory = new Category();
            newCategory.CategoryName = categoryRequestDto.CategoryName;


            _libraryContext.Add(newCategory);

            if(_libraryContext.SaveChanges() == 1)
            {
                return true;
            }

            return false; // Better solution would be to log the exception to SeriLog.

        }

        public Category EditCategory(int id, CategoryRequestDto categoryRequestDto)
        {
            var category = _libraryContext.Categories.FirstOrDefault(x => x.Id == id);

            if(category == null)
            {
                return null; // Category was not found.
            }

            if (DuplicateCategory(categoryRequestDto.CategoryName))
            {
                return null; // CategoryName already exists.
            }

            category.CategoryName = categoryRequestDto.CategoryName;

            if(_libraryContext.SaveChanges() == 1)
            {
                return category;
            }

            return null; // Better solution would be to log the exception to SeriLog.

        }

        public bool DeleteCategory(int id)
        {
            var category = _libraryContext.Categories.FirstOrDefault(x => x.Id == id);

            if(category == null)
            {
                return false; // Category was not found.
            }

            var libraryItem = _libraryContext.LibraryItems.FirstOrDefault(x => x.CategoryId == id);

            if(libraryItem != null)
            {
                return false; // There's a libraryItem that has a reference to the category where trying to remove.
            }

            _libraryContext.Categories.Remove(category);

            if(_libraryContext.SaveChanges() == 1)
            {
                return true;
            }

            return false; // Better solution would be to log the exception to SeriLog.

        }

        private bool DuplicateCategory(string categoryName) // A private helper method to assist 
            //with the checking of possible duplication of categoryName
        {
            var category = _libraryContext.Categories.FirstOrDefault(x => x.CategoryName == categoryName);

            if (category != null)
            {
                return true;
            }

            return false;
        }

    }
}
