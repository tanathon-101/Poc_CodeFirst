using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Entities
{
    public class EmployeeTask
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long TaskId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    }
}