using Microsoft.EntityFrameworkCore;
using EmployeeMIS.Models;

namespace EmployeeMIS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<PerformanceReport> PerformanceReports { get; set; }
    }
}   