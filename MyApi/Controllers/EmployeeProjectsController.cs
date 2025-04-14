using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.DTO.EmployeeProjectDTO;
using MyApi.Entities;
using MyApi.Entities.Employees;

[Route("api/[controller]")]
[ApiController]
public class EmployeeProjectsController : ControllerBase
{
    private readonly EmployeeContext _context;
    public EmployeeProjectsController(EmployeeContext context) => _context = context;

    // POST: api/employeeprojects
    [HttpPost]
    public async Task<IActionResult> AssignToProject([FromBody] AssignEmployeeToProjectRequest request)
    {
        var entity = new EmployeeProject
        {
            EmployeeId = request.EmployeeId,
            ProjectId = request.ProjectId
        };

        _context.EmployeeProjects.Add(entity);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // DELETE: api/employeeprojects?employeeId=1&projectId=2
    [HttpDelete]
    public async Task<IActionResult> RemoveFromProject([FromQuery] long employeeId, [FromQuery] long projectId)
    {
        var record = await _context.EmployeeProjects
            .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);

        if (record == null) return NotFound();

        _context.EmployeeProjects.Remove(record);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // GET: api/employeeprojects/by-employee/1
    [HttpGet("by-employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<EmployeeProjectResponse>>> GetProjectsForEmployee(long employeeId)
    {
        var projects = await _context.EmployeeProjects
            .Where(ep => ep.EmployeeId == employeeId)
            .Include(ep => ep.Project)
            .Select(ep => new EmployeeProjectResponse
            {
                ProjectId = ep.Project.ProjectId,
                ProjectName = ep.Project.ProjectName
            })
            .ToListAsync();

        return Ok(projects);
    }
}
