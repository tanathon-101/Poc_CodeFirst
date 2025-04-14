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
            .WithMany() // ❌ ไม่มี Project.EmployeeProjects แล้ว
            .HasForeignKey(ep => ep.ProjectId);

        modelBuilder.Entity<EmployeeProject>()
            .HasOne<Employee>() // ไม่มี ep.Employee ใน model แล้ว
            .WithMany(e => e.EmployeeProjects)
            .HasForeignKey(ep => ep.EmployeeId);

        // 🔗 One-to-One: Employee <-> EmployeeAddress
        modelBuilder.Entity<Employee>()
        .HasOne(e => e.Address)
        .WithOne() // 👈 ไม่มี a => a.Employee แล้ว
        .HasForeignKey<EmployeeAddress>(a => a.EmployeeId);

        // 📌 One-to-Many: Employee -> Tasks
        modelBuilder.Entity<EmployeeTask>()
            .HasOne<Employee>() // ไม่มี t.Employee แล้ว ต้องใช้แบบนี้
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.EmployeeId);

        // ✅ One-to-Many (unidirectional): Employee -> Department
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany() // ไม่มี Department.Employees แล้ว
            .HasForeignKey(e => e.DepartmentId);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<EmployeeProject> EmployeeProjects { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    public DbSet<EmployeeTask> EmployeeTasks { get; set; }
}