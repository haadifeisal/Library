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
        ICollection<EmployeeResponseDto> GetCollectionOfEmployees();
        Employee CreateEmployee(EmployeeRequestDto employeeRequestDto);
        Employee UpdateEmployee(int employeeId, EmployeeUpdateRequestDto employeeDto);
        bool DeleteEmployee(int employeeId);
    }
}
