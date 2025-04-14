using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using MyApi.Entities;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class EmployeeProjectsController : ControllerBase
{
    private readonly EmployeeContext _context;
    public EmployeeProjectsController(EmployeeContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> AssignToProject(EmployeeProject model)
    {
        _context.EmployeeProjects.Add(model);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFromProject(long employeeId, long projectId)
    {
        var record = await _context.EmployeeProjects
            .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);

        if (record == null) return NotFound();

        _context.EmployeeProjects.Remove(record);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("by-employee/{employeeId}")]
    public async Task<IActionResult> GetProjectsForEmployee(long employeeId)
    {
        var projects = await _context.EmployeeProjects
            .Where(ep => ep.EmployeeId == employeeId)
            .Include(ep => ep.Project)
            .ToListAsync();

        return Ok(projects);
    }
}