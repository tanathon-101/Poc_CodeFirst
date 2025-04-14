namespace MyApi.DTO.EmployeeTaskDTO
{
    public class EmployeeTaskResponse
    {
         public long TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; } // Optional เพิ่ม context ให้ response
    }
}