using Microsoft.AspNetCore.Mvc;
using MyApi.Entities;
using Microsoft.EntityFrameworkCore;
using MyApi.DTO.DepartmenDTO;
using MyApi.Entities.Projects;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public DepartmentController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentResponse>>> GetAllDepartments()
        {
            var departments = await _context.Departments
                .Select(d => new DepartmentResponse
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name
                })
                .ToListAsync();

            return Ok(departments);
        }

        // POST: api/department
        [HttpPost]
        public async Task<ActionResult<DepartmentResponse>> CreateDepartment(CreateDepartmentRequest departmentDto)
        {
            // Mapping จาก DTO ไปเป็น Entity
            var departmentEntity = new Department
            {
                Name = departmentDto.Name
            };

            _context.Departments.Add(departmentEntity);
            await _context.SaveChangesAsync();

            // Mapping กลับจาก Entity ไปเป็น DTO (ตอนส่งกลับให้ client)
            var resultDto = new DepartmentResponse
            {
                DepartmentId = departmentEntity.DepartmentId,
                Name = departmentEntity.Name
            };

            return CreatedAtAction(nameof(GetDepartment), new { id = departmentEntity.DepartmentId }, resultDto);
        }


        // GET: api/department/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(long id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            return department;
        }

        // PUT: api/department/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(long id, UpdateDepartmentRequest updateDto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            // อัปเดตเฉพาะ field ที่รับมาจาก request
            department.Name = updateDto.Name;

            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/department/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(long id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
