public interface IEmployeeProcess
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(int id, Employee employeeData);
    Task DeleteEmployeeAsync(int id);
}