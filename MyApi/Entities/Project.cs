using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Entities
{
   public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ProjectId { get; set; }

    public string ProjectName { get; set; }

    // Many-to-Many with Employee
    public ICollection<EmployeeProject> EmployeeProjects { get; set; }
}

    
}