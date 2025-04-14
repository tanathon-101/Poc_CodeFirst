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
}