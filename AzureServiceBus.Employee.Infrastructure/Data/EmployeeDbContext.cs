using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBus.Employee.Infrastructure.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entities.Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Employee>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Entities.Employee>().HasData(
                    new Entities.Employee
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Sandeep",
                        LastName = "Kumar",
                        DepartmentName = "Dot Net",
                        Address = "",
                        Contact = "",
                        JoiningDate = DateTime.Now,
                        EmpCode = Guid.NewGuid().ToString(),
                        IsActive = true,
                        CreatedBy = "",
                        CreatedOn = DateTime.Now,
                        ModifiedBy = "",
                        ModifiedOn = DateTime.Now
                    },
                    new Entities.Employee
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Praveen",
                        LastName = "Kumar",
                        DepartmentName = "QA",
                        Address = "",
                        Contact = "",
                        JoiningDate = DateTime.Now,
                        EmpCode = Guid.NewGuid().ToString(),
                        IsActive = true,
                        CreatedBy = "",
                        CreatedOn = DateTime.Now,
                        ModifiedBy = "",
                        ModifiedOn = DateTime.Now
                    },
                    new Entities.Employee
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Mukul",
                        LastName = "Bansal",
                        DepartmentName = "Sales",
                        Address = "",
                        Contact = "",
                        JoiningDate = DateTime.Now,
                        EmpCode = Guid.NewGuid().ToString(),
                        IsActive = true,
                        CreatedBy = "",
                        CreatedOn = DateTime.Now,
                        ModifiedBy = "",
                        ModifiedOn = DateTime.Now
                    }
                );
        }
    }
}
