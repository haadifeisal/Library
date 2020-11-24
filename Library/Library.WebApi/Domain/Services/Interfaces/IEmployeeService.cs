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
        Task<ICollection<EmployeeResponseDto>> GetCollectionOfEmployees();
        Task<Employee> CreateEmployee(EmployeeRequestDto employeeRequestDto);
        Task<Employee> UpdateEmployee(int employeeId, EmployeeUpdateRequestDto employeeDto);
        Task<bool> DeleteEmployee(int employeeId);
    }
}
