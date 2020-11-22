using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Helpers;
using Library.WebApi.Domain.Services;
using Library.WebApi.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebApi.Test.UnitTests
{
    [TestClass]
    public class UnitTestEmployeeService : UnitTestBase
    {
        [TestMethod]// Testing if we can create a Regular employee object.
        public void Verify_CreateEmployee_Returns_The_Correct_Regular_Employee_Object()
        {
            //Arrange
            var newEmployee = new Employee
            {
                FirstName = "Michael",
                LastName = "Harvey",
                IsManager = true,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager")
            };
            _context.Add(newEmployee);
            _context.SaveChanges();

            var employeeRequestDto = new EmployeeRequestDto
            {
                FirstName = "Johhny",
                LastName = "Bravo",
                IsManager = false,
                IsCeo = false,
                ManagerId = newEmployee.Id,
                Rank = 7
            };

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var employee = employeeService.CreateEmployee(employeeRequestDto);

            //Assert
            Assert.IsNotNull(employee);
            Assert.AreEqual(employee.FirstName, employeeRequestDto.FirstName);
            Assert.IsFalse(employee.IsCeo);
            Assert.IsFalse(employee.IsManager);
        }

        [TestMethod]
        public void Verify_CreateEmployee_Returns_Null_When_Regular_Employee_Is_Not_Managed_By_A_Manager()
        {
            //Arrange

            var employeeRequestDto = new EmployeeRequestDto
            {
                FirstName = "Johhny",
                LastName = "Bravo",
                IsManager = false,
                IsCeo = false,
                Rank = 7
            };

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var employee = employeeService.CreateEmployee(employeeRequestDto);

            //Assert
            Assert.IsNull(employee);
        }

        [TestMethod] // Testing if we can create a Manager employee that is managed by a CEO.
        public void Verify_CreateEmployee_Returns_The_Correct_Manager_Employee_Object()
        {
            //Arrange

            var newEmployee = new Employee
            {
                FirstName = "Michael",
                LastName = "Harvey",
                IsManager = false,
                IsCeo = true,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo")
            };
            _context.Add(newEmployee);
            _context.SaveChanges();

            var employeeRequestDto = new EmployeeRequestDto
            {
                FirstName = "Johhny",
                LastName = "Bravo",
                IsManager = true,
                IsCeo = false,
                Rank = 5,
                ManagerId = newEmployee.Id
            };

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var employee = employeeService.CreateEmployee(employeeRequestDto);

            //Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.IsManager);
            Assert.AreEqual(employee.ManagerId, newEmployee.Id);
        }

        [TestMethod] // Testing if we can create a CEO employee.
        public void Verify_CreateEmployee_Returns_The_Correct_CEO_Employee_Object()
        {
            //Arrange

            var employeeRequestDto = new EmployeeRequestDto
            {
                FirstName = "Johhny",
                LastName = "Bravo",
                IsManager = false,
                IsCeo = true,
                Rank = 10
            };

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var employee = employeeService.CreateEmployee(employeeRequestDto);

            //Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.IsCeo);
        }

        [TestMethod] // Testing if we can create another CEO employee when there's already a CEO.
        public void Verify_CreateEmployee_Returns_Null_When_Theres_Already_A_CEO()
        {
            //Arrange

            var newEmployee = new Employee
            {
                FirstName = "Michael",
                LastName = "Harvey",
                IsManager = false,
                IsCeo = true,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo")
            };
            _context.Add(newEmployee);
            _context.SaveChanges();

            var employeeRequestDto = new EmployeeRequestDto
            {
                FirstName = "Johhny",
                LastName = "Bravo",
                IsManager = false,
                IsCeo = true,
                Rank = 10
            };

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var employee = employeeService.CreateEmployee(employeeRequestDto);

            //Assert
            Assert.IsNull(employee);
        }

    }
}
