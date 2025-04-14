
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApi.Entities;

public class Employee
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EmployeeId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    // Foreign Key
    public long? DepartmentId { get; set; }
    public Department Department { get; set; }

    public EmployeeAddress Address { get; set; }
    public ICollection<EmployeeTask> Tasks { get; set; }
    public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }