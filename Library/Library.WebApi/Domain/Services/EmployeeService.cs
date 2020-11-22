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

        public Employee CreateEmployee(EmployeeRequestDto employeeRequestDto)
        {
            if(employeeRequestDto.IsManager == false && employeeRequestDto.IsCeo == false) // Checking if the employee is a regular employee.
            {
                if(employeeRequestDto.ManagerId != null)
                {
                    var manager = _libraryContext.Employees.AsNoTracking().FirstOrDefault(x => x.Id == employeeRequestDto.ManagerId
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
                    var manager = _libraryContext.Employees.AsNoTracking().FirstOrDefault(x => x.Id == employeeRequestDto.ManagerId
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

            var employeeIsCeo = _libraryContext.Employees.AsNoTracking().FirstOrDefault(x => x.IsCeo == true);

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
