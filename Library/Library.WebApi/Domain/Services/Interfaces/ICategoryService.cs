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
        Task<ICollection<Category>> GetCollectionOfCategories();
        Task<bool> CreateCategory(CategoryRequestDto categoryRequestDto);
        Task<Category> EditCategory(int id, CategoryRequestDto categoryRequestDto);
        Task<bool> DeleteCategory(int id);
    }
}
