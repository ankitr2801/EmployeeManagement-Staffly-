using EmployeeManangement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManangement.Infrastructure
{
    public class EmployeemanagementDbContext : DbContext
	{

        public EmployeemanagementDbContext(DbContextOptions<EmployeemanagementDbContext> Options) : base(Options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
