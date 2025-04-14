namespace MyApi.DTO.EmployeesDTO
{
    public class TaskDto
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}