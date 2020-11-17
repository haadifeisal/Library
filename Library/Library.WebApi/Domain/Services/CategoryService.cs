using Library.WebApi.Domain.Services.Interfaces;
using Library.WebApi.Repository;
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

        public bool AddCategory(string categoryName)
        {
            var category = _libraryContext.Categories.FirstOrDefault(x => x.CategoryName == categoryName);

            if(category != null)
            {
                return false;
            }

            var newCategory = new Category();
            newCategory.CategoryName = categoryName;


            _libraryContext.Add(newCategory);

            if(_libraryContext.SaveChanges() == 1)
            {
                return true;
            }

            return false; // Could not savechanges to db, exception error.

        }
    }
}
