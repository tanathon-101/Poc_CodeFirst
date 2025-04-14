using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Entities.Projects
{
    public class Department
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long DepartmentId { get; set; }
    
    public string Name { get; set; }

    }
}