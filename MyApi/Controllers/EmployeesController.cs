using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;
using MyApi.Entities;
using MyApi.Entities.Employees;
using MyApi.DTO.EmployeesDTO;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeContext _context;

    public EmployeesController(EmployeeContext context)
    {
        _context = context;
    }

    // GET: api/employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employees = await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Address)
            .Include(e => e.EmployeeProjects).ThenInclude(ep => ep.Project)
            .Include(e => e.Tasks)
            .ToListAsync();


        return Ok(employees);
    }


    // GET: api/employees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(long id)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Address)
            .Include(e => e.EmployeeProjects)
                .ThenInclude(ep => ep.Project)
            .Include(e => e.Tasks)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);

        if (employee == null)
            return NotFound();

        return employee;
    }

    // POST: api/employees
    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            DepartmentId = request.DepartmentId,
            Address = new EmployeeAddress
            {
                Street = request.Address?.Street,
                City = request.Address?.City,
                PostalCode = request.Address?.PostalCode
            },
            Tasks = request.Tasks?.Select(t => new EmployeeTask
            {
                Title = t.Title,
                Description = t.Description,

            }).ToList(),
            EmployeeProjects = request.EmployeeProjects?.Select(p => new EmployeeProject
            {
                ProjectId = p.ProjectId
            }).ToList()
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
    }

    [HttpPost("address")] // 👉 จะเป็น POST: api/employees/address
    [ActionName("CreateEmployeeAddress")]
    public async Task<ActionResult<EmployeeResponse>> CreateEmployeeAddress([FromBody] CreateEmployeeAdreesRequest request)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            DepartmentId = request.DepartmentId,
            Address = new EmployeeAddress
            {
                Street = request.Address?.Street,
                City = request.Address?.City,
                PostalCode = request.Address?.PostalCode
            }
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        var response = new EmployeeResponse
        {
            EmployeeId = employee.EmployeeId,
            FullName = $"{employee.FirstName} {employee.LastName}",
            Email = employee.Email,
            Address = new AddressDto
            {
                Street = employee.Address?.Street,
                City = employee.Address?.City,
                PostalCode = employee.Address?.PostalCode
            }
        };

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, response);
    }


    // PUT: api/employees/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(long id, Employee employee)
    {
        if (id != employee.EmployeeId)
            return BadRequest();

        _context.Entry(employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Employees.Any(e => e.EmployeeId == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    // DELETE: api/employees/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(long id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound();

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
