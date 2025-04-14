using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApi.Entities.Employees;

namespace MyApi.Entities.Projects
{
   public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ProjectId { get; set; }

    public string ProjectName { get; set; }

}

    
}