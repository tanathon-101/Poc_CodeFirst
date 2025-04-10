using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations; // Added for Swagger annotations

[ApiController]
[Route("api/[controller]")] // No version
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeProcess _employeeProcess;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeProcess employeeProcess,
     ILogger<EmployeesController> logger)
    {
        _employeeProcess = employeeProcess ?? throw new ArgumentNullException(nameof(employeeProcess));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("test")]   
    public IActionResult Test() => Ok("Hello");
    // GET: api/Employees
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Get all employees",
        Description = "Retrieves a list of all employees in the system",
        OperationId = "GetAllEmployees"
    )]
    public async Task<IActionResult> GetEmployees()
    {
        try
        {
            var employees = await _employeeProcess.GetAllEmployeesAsync();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all employees");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }

    // GET: api/Employees/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Get employee by ID",
        Description = "Retrieves a specific employee by their ID",
        OperationId = "GetEmployeeById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Employee found", typeof(Employee))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Employee not found")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        try
        {
            var employee = await _employeeProcess.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Employee not found");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting employee with ID {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }

    // POST: api/Employees
    [HttpPost]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Create a new employee",
        Description = "Creates a new employee in the system",
        OperationId = "CreateEmployee"
    )]
    public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEmployee = await _employeeProcess.CreateEmployeeAsync(employee);
            
            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = createdEmployee.EmployeeId },
                createdEmployee);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DuplicateEntityException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new employee");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }

    // PUT: api/Employees/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Update an existing employee",
        Description = "Updates an employee's details",
        OperationId = "UpdateEmployee"
    )]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                return BadRequest("ID mismatch between URL and request body");
            }

            await _employeeProcess.UpdateEmployeeAsync(id, employee);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DuplicateEntityException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating employee with ID {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }

    // DELETE: api/Employees/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Delete an employee",
        Description = "Removes an employee from the system",
        OperationId = "DeleteEmployee"
    )]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            await _employeeProcess.DeleteEmployeeAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting employee with ID {id}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }
}