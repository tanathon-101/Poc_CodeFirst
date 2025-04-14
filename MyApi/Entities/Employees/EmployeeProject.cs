using MyApi.Entities.Projects;

namespace MyApi.Entities.Employees
{
    public class EmployeeProject
    {
    public long EmployeeId { get; set; }
    public long ProjectId { get; set; }
    public Project Project { get; set; }
    }
}