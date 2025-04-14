public class CreateEmployeeRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public long? DepartmentId { get; set; }

    public EmployeeAddressRequest Address { get; set; }
    public List<EmployeeTaskRequest> Tasks { get; set; }
    public List<EmployeeProjectRequest> EmployeeProjects { get; set; }
}

public class EmployeeAddressRequest
{
    public string Street { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
}

public class EmployeeTaskRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}

public class EmployeeProjectRequest
{
    public long ProjectId { get; set; }
}
