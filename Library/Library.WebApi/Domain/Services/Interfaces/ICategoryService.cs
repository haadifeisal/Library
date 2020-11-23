using Library.WebApi.DataTransferObject;
using Library.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Domain.Services.Interfaces
{
    public interface ICategoryService
    {
        ICollection<Category> GetCollectionOfCategories();
        bool CreateCategory(CategoryRequestDto categoryRequestDto);
        Category EditCategory(int id, CategoryRequestDto categoryRequestDto);
        bool DeleteCategory(int id);
    }
}
