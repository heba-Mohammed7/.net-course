using ApiProject.Data;
using ApiProject.Dto;
using ApiProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Controller;
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public DepartmentController(ApplicationDbContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var department = mapper.Map<Department>(departmentDto);
        await context.Departments.AddAsync(department, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
    }

    [HttpGet("{id}")]
    public IActionResult GetDepartmentById(int id)
    {
        var department = context.Departments
            .Include(d => d.Employees)
            .ThenInclude(e => e.Role)
            .FirstOrDefault(d => d.Id == id);
    
        if (department == null)
        {
            return NotFound();
        }
    
        var departmentDto = new DepartmentResponseDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            Employees = department.Employees.Select(e => mapper.Map<EmployeeResponseDto>(e)).ToList()
        };
    
        return Ok(departmentDto);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteDepartment(int id)
    {
        var department = context.Departments.FirstOrDefault(d => d.Id == id);
        if (department == null)
        {
            return NotFound();
        }
        
        context.Departments.Remove(department);
        context.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateDepartment(int id, DepartmentDto updatedDepartment)
    {
        var department = context.Departments.FirstOrDefault(d => d.Id == id);
        if (department == null)
        {
            return NotFound();
        }
        
        var newDepartment = mapper.Map(updatedDepartment, department);
        context.Departments.Update(newDepartment);
        context.SaveChanges();
        
        return Ok(department);
    }
}