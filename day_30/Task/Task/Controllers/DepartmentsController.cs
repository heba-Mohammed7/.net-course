using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Dto;
using Task.interfaces;
using Task.Models;

namespace Task.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController(ApplicationDbContext _context, IMapper _mapper, IGenericRepository<Department> _repository) : ControllerBase
{


    [HttpPost]
    public async Task<IActionResult> CreateDepartment(DepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var department = _mapper.Map<Department>(departmentDto);
        _repository.Add(department);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok(department.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments(int pageNumber = 1, int pageSize = 10)
    {
        var query = _repository.GetQueryable()
            .Include(d => d.Employees)
            .Include(d => d.Manager);
        var items = await query.OrderBy(d => d.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var departmentDtos = _mapper.Map<IEnumerable<DepartmentResponseDto>>(items);
        return Ok(departmentDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        var department = await _repository.GetQueryable()
            .Include(d => d.Employees)
            .Include(d => d.Manager)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (department == null)
        {
            return NotFound();
        }
        var departmentDto = _mapper.Map<DepartmentResponseDto>(department);
        return Ok(departmentDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _repository.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _repository.Delete(department);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDepartment(int id, DepartmentDto updatedDepartmentDto, CancellationToken cancellationToken)
    {
        var department = await _repository.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _mapper.Map(updatedDepartmentDto, department);

        _repository.Update(department);
        await _context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}