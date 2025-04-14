using Microsoft.EntityFrameworkCore;
using MyApi.Entities;


public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Composite key for EmployeeProject
        modelBuilder.Entity<EmployeeProject>()
            .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeeProjects)
            .HasForeignKey(ep => ep.EmployeeId);

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Project)
            .WithMany(p => p.EmployeeProjects)
            .HasForeignKey(ep => ep.ProjectId);

        // 👇 One-to-One Employee <-> EmployeeAddress
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Address)
            .WithOne(a => a.Employee)
            .HasForeignKey<EmployeeAddress>(a => a.EmployeeId);

        // 👇 One-to-Many Employee -> Tasks
        modelBuilder.Entity<EmployeeTask>()
            .HasOne(t => t.Employee)
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.EmployeeId);

        // 👇 One-to-Many Department -> Employees
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId);
}

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<EmployeeProject> EmployeeProjects { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    public DbSet<EmployeeTask> EmployeeTasks { get; set; }
    }