using System.ComponentModel.DataAnnotations;

namespace MyApi.Entities
{
    public class EmployeeAddress
    {
    [Key]
    public long EmployeeId { get; set; } // Also acts as PK and FK

    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("EmployeeId")]

    public Employee Employee { get; set; }
    }
}