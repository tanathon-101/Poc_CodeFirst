using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Entities
{
    public class Department
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long DepartmentId { get; set; }
    
    public string Name { get; set; }

    // Navigation property
    public ICollection<Employee> Employees { get; set; }
    }
}