using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.DTO.EmployeeTaskDTO;
using MyApi.Entities;
using MyApi.Entities.Employees;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public TaskController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/employeetask
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<EmployeeTaskResponse>>> GetAllTasks()
        {
            var tasks = await (from task in _context.EmployeeTasks
                               join emp in _context.Employees
                               on task.EmployeeId equals emp.EmployeeId
                               select new EmployeeTaskResponse
                               {
                                   TaskId = task.TaskId,
                                   Title = task.Title,
                                   Description = task.Description,
                                   EmployeeId = emp.EmployeeId,
                                   EmployeeName = $"{emp.FirstName} {emp.LastName}"
                               }).ToListAsync();

            return Ok(tasks);
        }

        // GET: api/employeetask/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTaskResponse>> GetTask(long id)
        {
            var task = await _context.EmployeeTasks
                .FirstOrDefaultAsync(t => t.TaskId == id);

            if (task == null)
                return NotFound();

            var response = new EmployeeTaskResponse
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                EmployeeId = task.EmployeeId,
            };

            return Ok(response);
        }

        // POST: api/employeetask
        [HttpPost]
        public async Task<ActionResult<EmployeeTaskResponse>> CreateTask(CreateEmployeeTaskRequest request)
        {
            var task = new EmployeeTask
            {
                Title = request.Title,
                Description = request.Description,
                EmployeeId = request.EmployeeId
            };

            _context.EmployeeTasks.Add(task);
            await _context.SaveChangesAsync();

            string employeeName = null;
            if (task.EmployeeId.HasValue)
            {
                var employee = await _context.Employees.FindAsync(task.EmployeeId);
                if (employee != null)
                {
                    employeeName = $"{employee.FirstName} {employee.LastName}";
                }
            }

            var response = new EmployeeTaskResponse
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                EmployeeId = task.EmployeeId,
                EmployeeName = employeeName
            };

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, response);
        }
        [HttpPut("assign/{taskId}")]
        public async Task<IActionResult> AssignEmployeeToTask(long taskId, [FromBody] AssignEmployeeToTaskRequest request)
        {
            var task = await _context.EmployeeTasks.FindAsync(taskId);
            if (task == null)
                return NotFound();

            task.EmployeeId = request.EmployeeId;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/employeetask/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(long id, UpdateEmployeeTaskRequest request)
        {
            var task = await _context.EmployeeTasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Title = request.Title;
            task.Description = request.Description;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/employeetask/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(long id)
        {
            var task = await _context.EmployeeTasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.EmployeeTasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
