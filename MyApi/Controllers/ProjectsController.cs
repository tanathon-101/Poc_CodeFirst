using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.DTO.ProjectDTO;
using MyApi.Entities;
using MyApi.Entities.Projects;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public ProjectController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAllProjects()
        {
            var projects = await _context.Projects
                .Select(p => new ProjectResponse
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName
                })
                .ToListAsync();

            return Ok(projects);
        }

        // GET: api/project/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponse>> GetProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return NotFound();

            return new ProjectResponse
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName
            };
        }

        // POST: api/project
        [HttpPost]
        public async Task<ActionResult<ProjectResponse>> CreateProject(CreateProjectRequest request)
        {
            var project = new Project
            {
                ProjectName = request.ProjectName
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var response = new ProjectResponse
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName
            };

            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, response);
        }

        // PUT: api/project/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(long id, UpdateProjectRequest request)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            project.ProjectName = request.ProjectName;

            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
