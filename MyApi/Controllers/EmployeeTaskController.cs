using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Entities;

namespace MyApi.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTaskController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeTaskController(EmployeeContext context)
        {
            _context = context;
        }

        // POST: api/EmployeeTask
        [HttpPost]
        public async Task<ActionResult<EmployeeTask>> AddTask(EmployeeTask task)
        {
            if (!_context.Employees.Any(e => e.EmployeeId == task.EmployeeId))
            {
                return BadRequest($"EmployeeId {task.EmployeeId} not found.");
            }

            _context.EmployeeTasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
        }

        // GET: api/EmployeeTask/5 (ใช้ใน CreatedAtAction)
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTask>> GetTask(long id)
        {
            var task = await _context.EmployeeTasks.FindAsync(id);

            if (task == null)
                return NotFound();

            return task;
        }

        // PUT: api/EmployeeTask/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(long id, EmployeeTask task)
        {
            if (id != task.TaskId)
                return BadRequest();

            if (!_context.Employees.Any(e => e.EmployeeId == task.EmployeeId))
            {
                return BadRequest($"EmployeeId {task.EmployeeId} not found.");
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.EmployeeTasks.Any(t => t.TaskId == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }
    }
}
