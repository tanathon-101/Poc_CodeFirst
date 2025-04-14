namespace MyApi.DTO.EmployeesDTO
{
    public class EmployeeResponse
    {
        public long EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DepartmentDto Department { get; set; }
        public AddressDto Address { get; set; }
        public List<ProjectDto> Projects { get; set; }
        public List<TaskDto> Tasks { get; set; }
        
    }
    public class DepartmentDto
    {
        public long DepartmentId { get; set; }
        public string Name { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class ProjectDto
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
    }

    public class TaskDto
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}