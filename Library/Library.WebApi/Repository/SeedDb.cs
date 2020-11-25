using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.WebApi.Domain.Helpers;

namespace Library.WebApi.Repository
{
    public static class SeedDb
    {
        public static void Populate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<LibraryContext>());
            }
        }

        public static void SeedData(LibraryContext context)
        {
            System.Console.WriteLine("\n\nApplying Migrations ...\n\n");

            context.Database.Migrate();

            if (!context.Categories.Any() && !context.LibraryItems.Any() && !context.Employees.Any())
            {
                
                System.Console.WriteLine("\n\nAdding data - Seeding ...\n\n");

                var category = new Category{ CategoryName = "Action"};
                context.Add(category);

                var category2 = new Category { CategoryName = "Drama" };
                context.Add(category2);

                context.SaveChanges();

                var libraryItem = new LibraryItem { CategoryId = category.Id, Type = "book",  Title = "Sunset in the mountains", Author = "James Parkley", Pages = 134, Borrower = "", IsBorrowable = true };
                context.Add(libraryItem); // Book

                var libraryItem2 = new LibraryItem { CategoryId = category2.Id, Type = "audio book", Title = "Teletubies", Author = "", RunTimeMinutes = 45, Borrower = "", IsBorrowable = true };
                context.Add(libraryItem2); // Audio Book

                var libraryItem3 = new LibraryItem { CategoryId = category2.Id, Type = "dvd", Title = "Men In Black", Author = "",RunTimeMinutes = 88, Borrower = "", IsBorrowable = true };
                context.Add(libraryItem3); // Dvd

                var libraryItem4 = new LibraryItem { CategoryId = category.Id, Type = "reference book", Title = "The Alchemist", Author = "Paulo Choelo", Pages = 70, Borrower = "", IsBorrowable = false };
                context.Add(libraryItem4); // Reference Book

                var employee = new Employee { FirstName = "Cristiano", LastName = "Ronaldo", IsCeo = true, IsManager = false, Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(10, "ceo") };
                context.Add(employee);

                var employee2 = new Employee { FirstName = "Thierry", LastName = "Henry", IsCeo = false, IsManager = true, Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(8, "manager") };
                context.Add(employee2);

                var employee3 = new Employee { FirstName = "Ronaldo", LastName = "Lima", IsCeo = false, IsManager = true, Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(7, "manager"), ManagerId = employee2.Id };
                context.Add(employee3);

                var employee4 = new Employee { FirstName = "Andres", LastName = "Iniesta", IsCeo = false, IsManager = false, Salary = (decimal)CalculateSalary.CalculateEmployeeSalary(5, "employee"), ManagerId = employee3.Id };
                context.Add(employee4);

                context.SaveChanges();

                System.Console.WriteLine("\n\nMigrations worked !! ...\n\n");
            }
            else
            {
                System.Console.WriteLine("Already have data - not seeding");
            }
        }


    }
}
