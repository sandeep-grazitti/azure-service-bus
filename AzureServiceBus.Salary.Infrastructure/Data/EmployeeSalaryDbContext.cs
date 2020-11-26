using System;
using AzureServiceBus.Salary.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AzureServiceBus.Salary.Infrastructure.Data
{
    public class EmployeeSalaryDbContext : DbContext
    {
        public EmployeeSalaryDbContext(DbContextOptions<EmployeeSalaryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.EmployeeSalary>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            });
        }
    }
}
