using Library.WebApi.DataTransferObject;
using Library.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Domain.Services.Interfaces
{
    public interface IEmployeeService
    {
        Employee CreateEmployee(EmployeeRequestDto employeeRequestDto);
    }
}
