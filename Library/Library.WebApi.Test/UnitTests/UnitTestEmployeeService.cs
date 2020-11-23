using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Helpers;
using Library.WebApi.Domain.Services;
using Library.WebApi.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.WebApi.Test.UnitTests
{
    [TestClass]
    public class UnitTestEmployeeService : UnitTestBase
    {
        [TestMethod]// Testing if we can create a Regular employee object.
        public void Verify_GetCollectionOfEmployees_Returns_A_Collection_Of_Employees_Grouped_By_Role()
        {
            //Arrange
            var newEmployee = new Employee
            {
                FirstName = "Michael",
                LastName = "Harvey",
                IsManager = false,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(5, "employee")
            };
            _context.Add(newEmployee);

            var newEmployee2 = new Employee
            {
                FirstName = "James",
                LastName = "Bond",
                IsManager = true,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager")
            };
            _context.Add(newEmployee2);

            var newEmployee3 = new Employee
            {
                FirstName = "Big",
                LastName = "Boss",
                IsManager = false,
                IsCeo = true,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo")
            };
            _context.Add(newEmployee3);

            _context.SaveChanges();

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var employeeCollection = employeeService.GetCollectionOfEmployees();

            //Assert
            Assert.IsTrue(employeeCollection.Count > 1);
            Assert.AreEqual(employeeCollection.First().Role, "ceo");
            Assert.AreEqual(employeeCollection.Last().Role, "manager");
        }

        [TestMethod]// Testing if we can create a Regular employee object.
        public void Verify_GetCollectionOfEmployees_Returns_An_Empty_Collection_When_Theres_No_Employees_Found()
        {
            //Arrange
            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var employeeCollection = employeeService.GetCollectionOfEmployees();

            //Assert
            Assert.AreEqual(employeeCollection.Count, 0);
        }

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

        [TestMethod]// Testing if we can update a regular employee.
        public void Verify_EditEmployee_Returns_The_Correct_Edited_Employee_Object()
        {
            //Arrange
            var newEmployee = new Employee
            {
                FirstName = "Michael",
                LastName = "Harvey",
                IsManager = false,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(5, "employee")
            };
            _context.Add(newEmployee);

            var newEmployee2 = new Employee
            {
                FirstName = "James",
                LastName = "Bond",
                IsManager = true,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager")
            };
            _context.Add(newEmployee2);

            var newEmployee3 = new Employee
            {
                FirstName = "Big",
                LastName = "Boss",
                IsManager = false,
                IsCeo = true,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo")
            };
            _context.Add(newEmployee3);

            _context.SaveChanges();

            var editEmployeeDto = new EmployeeUpdateRequestDto
            {
                FirstName = "Fernando",
                LastName = "",
                Rank = 6,
                ManagerId = newEmployee2.Id
            };

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var editedEmployee = employeeService.UpdateEmployee(newEmployee.Id, editEmployeeDto);

            //Assert
            Assert.IsNotNull(editedEmployee);
            Assert.AreEqual(editedEmployee.FirstName, "Fernando");
            Assert.AreEqual(editedEmployee.ManagerId, newEmployee2.Id);
        }

        [TestMethod]// Testing if we can update a manager, the manager will be managed by a ceo.
        public void Verify_EditEmployee_Returns_The_Edited_Manager_Employee_Object()
        {
            //Arrange
            var newEmployee2 = new Employee
            {
                FirstName = "James",
                LastName = "Bond",
                IsManager = true,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager")
            };
            _context.Add(newEmployee2);

            var newEmployee3 = new Employee
            {
                FirstName = "Big",
                LastName = "Boss",
                IsManager = false,
                IsCeo = true,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo")
            };
            _context.Add(newEmployee3);

            _context.SaveChanges();

            var editEmployeeDto = new EmployeeUpdateRequestDto
            {
                FirstName = "James",
                LastName = "Bond",
                Rank = 0,
                ManagerId = newEmployee3.Id
            };

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var editedEmployee = employeeService.UpdateEmployee(newEmployee2.Id, editEmployeeDto);

            //Assert
            Assert.IsNotNull(editedEmployee);
            Assert.AreEqual(editedEmployee.ManagerId, newEmployee3.Id);
        }

        [TestMethod]// Testing if we can update a ceo. We should not be able to assign the CEO a manager who he can report to.
        public void Verify_EditEmployee_Returns_The_Edited_CEO_Employee_Object()
        {
            //Arrange
            var newEmployee2 = new Employee
            {
                FirstName = "James",
                LastName = "Bond",
                IsManager = true,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager")
            };
            _context.Add(newEmployee2);

            var newEmployee3 = new Employee
            {
                FirstName = "Big",
                LastName = "Boss",
                IsManager = false,
                IsCeo = true,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo")
            };
            _context.Add(newEmployee3);

            _context.SaveChanges();

            var editEmployeeDto = new EmployeeUpdateRequestDto
            {
                FirstName = "Gates",
                LastName = "Boss",
                Rank = 9,
                ManagerId = newEmployee2.Id
            };

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var editedEmployee = employeeService.UpdateEmployee(newEmployee3.Id, editEmployeeDto);

            //Assert
            Assert.IsNotNull(editedEmployee);
            Assert.IsNull(editedEmployee.ManagerId);
            Assert.AreEqual(editedEmployee.FirstName, "Gates");
        }

        [TestMethod] // Testing if we can delete a regular employee.
        public void Verify_DeleteEmployee_Returns_True_When_Deleting_A_Regular_Employee()
        {
            //Arrange
            var newEmployee = new Employee
            {
                FirstName = "Michael",
                LastName = "Harvey",
                IsManager = false,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(5, "employee")
            };
            _context.Add(newEmployee);

            var newEmployee2 = new Employee
            {
                FirstName = "James",
                LastName = "Bond",
                IsManager = true,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager")
            };
            _context.Add(newEmployee2);

            var newEmployee3 = new Employee
            {
                FirstName = "Big",
                LastName = "Boss",
                IsManager = false,
                IsCeo = true,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo")
            };
            _context.Add(newEmployee3);

            _context.SaveChanges();

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var deleteEmployee = employeeService.DeleteEmployee(newEmployee.Id);

            //Assert
            Assert.IsTrue(deleteEmployee);
        }

        [TestMethod] // Testing if we can delete a employee who is being managed by a manager.
        public void Verify_DeleteEmployee_Return_False_When_Trying_To_Delete_A_Employee_Who_Is_Being_Managed()
        {
            //Arrange

            var newEmployee = new Employee
            {
                FirstName = "Pablo",
                LastName = "Reyes",
                IsManager = true,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager")
            };
            _context.Add(newEmployee);

            var newEmployee2 = new Employee
            {
                FirstName = "James",
                LastName = "Bond",
                IsManager = true,
                IsCeo = false,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager"),
                ManagerId = newEmployee.Id
            };
            _context.Add(newEmployee2);

            var newEmployee3 = new Employee
            {
                FirstName = "Big",
                LastName = "Boss",
                IsManager = false,
                IsCeo = true,
                Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo")
            };
            _context.Add(newEmployee3);

            _context.SaveChanges();

            var employeeService = new EmployeeService(_context, _mapper);

            //Act
            var deleteEmployee = employeeService.DeleteEmployee(newEmployee.Id);

            //Assert
            Assert.IsFalse(deleteEmployee);
        }

    }
}
