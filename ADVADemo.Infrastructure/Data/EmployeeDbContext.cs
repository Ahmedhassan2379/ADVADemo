using ADVADemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ADVADemo.Infrastructure.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define one-to-one relationship between Department and its manager
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithOne()
                .HasForeignKey<Department>(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.DepartmentId);

            // Define self-referencing relationship in Employee for manager
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(e => e.EmployeesUnderManger)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cyclic delete


            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "IT" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Alice", Salary = 60000, DepartmentId = 1, ManagerId = null }, // Manager of HR
                new Employee { Id = 2, Name = "Bob", Salary = 70000, DepartmentId = 2, ManagerId = null }, // Manager of IT
                new Employee { Id = 3, Name = "Charlie", Salary = 40000, DepartmentId = 1, ManagerId = 1 }, // Reports to Alice
                new Employee { Id = 4, Name = "Dave", Salary = 45000, DepartmentId = 2, ManagerId = 2 }    // Reports to Bob
            );


            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
