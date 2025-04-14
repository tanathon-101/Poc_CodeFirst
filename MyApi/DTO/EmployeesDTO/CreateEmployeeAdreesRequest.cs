namespace MyApi.DTO.EmployeesDTO
{
    public class CreateEmployeeAdreesRequest
    {
        public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public long? DepartmentId { get; set; }
    public EmployeeAddressRequest Address { get; set; }
    }
}