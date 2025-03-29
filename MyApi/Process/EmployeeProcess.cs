public class EmployeeProcess :IEmployeeProcess
{
    private readonly IEmployeeRepository  _employeeRepository;

    public EmployeeProcess(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _employeeRepository.FindAllAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        var employee = await _employeeRepository.FindById(id);
        if (employee == null)
        {
            throw new NotFoundException($"Employee with ID {id} not found.");
        }
        return employee;
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        // Check for duplicate employee (e.g., by email)
        var existingEmployee = await _employeeRepository.FindBy(e => e.Email == employee.Email);
        if (existingEmployee != null)
        {
            throw new DuplicateEntityException($"Employee with email {employee.Email} already exists.");
        }

        await _employeeRepository.CreateAsync(employee);
        return employee;
    }

    public async Task UpdateEmployeeAsync(int id, Employee employeeData)
    {
        if (employeeData == null)
        {
            throw new ArgumentNullException(nameof(employeeData));
        }

        var employee = await _employeeRepository.FindById(id);
        if (employee == null)
        {
            throw new NotFoundException($"Employee with ID {id} not found.");
        }

        // Update employee properties
        employee.FirstName = employeeData.FirstName;
        employee.LastName = employeeData.LastName;
        employee.Email = employeeData.Email;
       

        // Check for duplicate email with other employees
        var existingEmployee = await _employeeRepository.FindBy(e => e.Email == employee.Email && e.EmployeeId != id);
        if (existingEmployee != null)
        {
            throw new DuplicateEntityException($"Another employee with email {employee.Email} already exists.");
        }

        _employeeRepository.Update(employee);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _employeeRepository.FindById(id);
        if (employee == null)
        {
            throw new NotFoundException($"Employee with ID {id} not found.");
        }

        _employeeRepository.Delete(employee);
    }
}
