using AutoMapper;
using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Helpers;
using Library.WebApi.Domain.Services.Interfaces;
using Library.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly LibraryContext _libraryContext;
        private readonly IMapper _mapper;
        public EmployeeService(LibraryContext libraryContext, IMapper mapper)
        {
            _libraryContext = libraryContext;
            _mapper = mapper;
        }

        public async Task<ICollection<EmployeeResponseDto>> GetCollectionOfEmployees()
        {
            var employeesCollection = await _libraryContext.Employees.AsNoTracking().ToListAsync();

            if (employeesCollection.Any())
            {
                var mappedEmployeeCollection = _mapper.Map<List<EmployeeResponseDto>>(employeesCollection);
                foreach(var employee in mappedEmployeeCollection)
                {
                    if(employee.IsManager == false && employee.IsCeo == false)
                    {
                        employee.Role = "employee";
                        continue;
                    }
                    if (employee.IsManager)
                    {
                        employee.Role = "manager";
                        continue;
                    }
                    if (employee.IsCeo)
                    {
                        employee.Role = "ceo";
                        continue;
                    }
                }

                mappedEmployeeCollection = mappedEmployeeCollection.OrderBy(x => x.Role).ToList();

                return mappedEmployeeCollection;
            }

            return new List<EmployeeResponseDto>();

        }

        public async Task<Employee> CreateEmployee(EmployeeRequestDto employeeRequestDto)
        {
            if(employeeRequestDto.IsCeo == true && employeeRequestDto.IsManager == true) //An employee can't be a manager
                // and a ceo at the same time.
            {
                return null;
            }

            if(employeeRequestDto.IsManager == false && employeeRequestDto.IsCeo == false) // Checking if the employee is a regular employee.
            {
                if(employeeRequestDto.ManagerId != null)
                {
                    var manager = await _libraryContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeRequestDto.ManagerId
                        && x.IsManager == true);

                    if(manager != null) // Checking if the manager that the employee can report to exists.
                    {
                        var employee = _mapper.Map<Employee>(employeeRequestDto);
                        employee.IsManager = false;
                        employee.IsCeo = false;
                        employee.Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(employeeRequestDto.Rank, "employee");

                        return SaveToDb(employee);
                    }

                    return null; // Return null if an employee is not managed by a manager.

                }
                
                return null; // A regular employee must have a manager to report to.
            }

            if(employeeRequestDto.IsManager == true) // Checking if the employee is a manager.
            {
                var employee = new Employee();
                if(employeeRequestDto.ManagerId != null) 
                {
                    var manager = await _libraryContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeRequestDto.ManagerId
                        && (x.IsManager == true || x.IsCeo == true)); // Manager must report to an existing manager.

                    if (manager != null) // Checking if the manager or ceo that the manager employee can report to exists
                    {
                        employee = _mapper.Map<Employee>(employeeRequestDto);
                        employee.IsCeo = false;
                        employee.Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(employeeRequestDto.Rank, "manager");

                        return SaveToDb(employee);
                    }

                }

                employee = _mapper.Map<Employee>(employeeRequestDto);
                employee.Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(employeeRequestDto.Rank, "manager");

                return SaveToDb(employee);
            }

            var employeeIsCeo = await  _libraryContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.IsCeo == true);

            if (employeeRequestDto.IsCeo == true) // Checking if the employee is a CEO.
            {
                if (employeeIsCeo != null) // Only one CEO at a time.
                {
                    return null;
                }

                var employee = _mapper.Map<Employee>(employeeRequestDto);
                employee.IsManager = false;
                employee.ManagerId = null;
                employee.Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(employeeRequestDto.Rank, "ceo");

                return SaveToDb(employee);
            }

            return null;
        }

        public async Task<Employee> UpdateEmployee(int employeeId, EmployeeUpdateRequestDto employeeDto)
        {
            var employee = await _libraryContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);

            if(employee == null)
            {
                return null; // Employee not found.
            }

            employee.FirstName = string.IsNullOrEmpty(employeeDto.FirstName) ? employee.FirstName : employeeDto.FirstName;
            employee.LastName = string.IsNullOrEmpty(employeeDto.LastName) ? employee.LastName : employeeDto.LastName;
            employee.Salary = employeeDto.Rank == 0 ? employee.Salary : (decimal)CalculateSalary.CalculateEmployeeSalary(employeeDto.Rank, "employee");

            if(employeeDto.ManagerId != null)
            {
                var managerEmployee = await _libraryContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeDto.ManagerId);
                if (managerEmployee != null)
                {
                    if (employee.IsManager == false && employee.IsCeo == false && managerEmployee.IsManager == true) 
                        // Checking if its a regular employee that will have a manager to report to.
                    {
                        employee.ManagerId = employeeDto.ManagerId;
                    }

                    if(employee.IsManager == true && (managerEmployee.IsManager == true || managerEmployee.IsCeo == true)) 
                        // Checking if its a manager who will have a manager or ceo to report to.
                    {
                        employee.ManagerId = employeeDto.ManagerId;
                    }
                }
            }

            _libraryContext.SaveChanges();

            return employee;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var employee = await _libraryContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);

            if(employee == null)
            {
                return false; // Employee not found.
            }

            if(employee.IsManager == true || employee.IsCeo == true) //Checking if the employee is a manager or a ceo.
            {
                
                if(await ManagedByOtherEmployee(employee.Id))
                {
                    return false; // Can't delete a manager or a ceo who is managing other employees.
                }

                _libraryContext.Remove(employee);
                _libraryContext.SaveChanges();

                return true;
            }

            // Regular employee
            _libraryContext.Remove(employee);
            _libraryContext.SaveChanges();

            return true;
        }

        private async Task<bool> ManagedByOtherEmployee(int employeeId)
        {
            var managingOtherEmployees = await _libraryContext.Employees.AnyAsync(x => x.ManagerId == employeeId);

            return managingOtherEmployees;
        }

        private Employee SaveToDb(Employee employee)
        {
            _libraryContext.Add(employee);

            if (_libraryContext.SaveChanges() == 1)
            {
                return employee;
            }

            return null; // Better solution would be to log the exception to SeriLog.
        }

    }
}
