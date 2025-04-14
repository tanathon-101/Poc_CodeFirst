using Microsoft.EntityFrameworkCore;
using MyApi.Entities;
using MyApi.Entities.Employees;
using MyApi.Entities.Projects;


public class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeProject>()
     .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Project)
            .WithMany() 
            .HasForeignKey(ep => ep.ProjectId);

        modelBuilder.Entity<EmployeeProject>()
            .HasOne<Employee>() 
            .WithMany(e => e.EmployeeProjects)
            .HasForeignKey(ep => ep.EmployeeId);

        modelBuilder.Entity<Employee>()
        .HasOne(e => e.Address)
        .WithOne() 
        .HasForeignKey<EmployeeAddress>(a => a.EmployeeId);

        modelBuilder.Entity<EmployeeTask>()
            .HasOne<Employee>() 
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.EmployeeId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<EmployeeProject> EmployeeProjects { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    public DbSet<EmployeeTask> EmployeeTasks { get; set; }
}