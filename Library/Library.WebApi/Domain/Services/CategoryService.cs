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

        public async Task<ICollection<Category>> GetCollectionOfCategories()
        {
            var categoryCollection = await _libraryContext.Categories.AsNoTracking().ToListAsync();

            return categoryCollection;
        }

        public async Task<bool> CreateCategory(CategoryRequestDto categoryRequestDto)
        {

            if(await DuplicateCategory(categoryRequestDto.CategoryName))
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

        public async Task<Category> EditCategory(int id, CategoryRequestDto categoryRequestDto)
        {
            var category = await _libraryContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(category == null)
            {
                return null; // Category was not found.
            }

            if (await DuplicateCategory(categoryRequestDto.CategoryName))
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

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _libraryContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(category == null)
            {
                return false; // Category was not found.
            }

            var libraryItem = await _libraryContext.LibraryItems.FirstOrDefaultAsync(x => x.CategoryId == id);

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

        private async Task<bool> DuplicateCategory(string categoryName) // A private helper method to assist 
            //with the checking of possible duplication of categoryName
        {
            var category = await _libraryContext.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);

            if (category != null)
            {
                return true;
            }

            return false;
        }

    }
}
