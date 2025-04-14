namespace MyApi.DTO.EmployeeTaskDTO
{
    public class CreateEmployeeTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long? EmployeeId { get; set; }
    }
}