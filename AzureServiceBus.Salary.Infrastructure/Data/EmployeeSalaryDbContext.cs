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

        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
