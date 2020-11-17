using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Domain.Services.Interfaces
{
    public interface ICategoryService
    {
        bool AddCategory(string categoryName);
    }
}
