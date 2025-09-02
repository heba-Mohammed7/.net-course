using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Dto;
using Task.interfaces;
using Task.Models;
using Task.Services;

namespace Task.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Employee> _repository;
    private readonly IFileUpload _fileUpload;

    public EmployeesController(ApplicationDbContext context, IMapper mapper, IGenericRepository<Employee> repository, IFileUpload fileUpload)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
        _fileUpload = fileUpload;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromForm] EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<Employee>(employeeDto);

        if (employeeDto.Image != null)
        {
            employee.ImagePath = await _fileUpload.UploadAsync(employeeDto.Image, "employee", cancellationToken);
        }

        _repository.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok(employee.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees(int pageNumber = 1, int pageSize = 10)
    {
        var query = _repository.GetQueryable()
            .Include(e => e.Department);
        var items = await query.OrderBy(e => e.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeResponseDto>>(items);
        return Ok(employeeDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _repository.GetQueryable()
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        var employeeDto = _mapper.Map<EmployeeResponseDto>(employee);
        return Ok(employeeDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _repository.Delete(employee);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromForm] EmployeeDto updatedEmployeeDto, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _mapper.Map(updatedEmployeeDto, employee);

        if (updatedEmployeeDto.Image != null)
        {
            employee.ImagePath = await _fileUpload.UploadAsync(updatedEmployeeDto.Image, "employee", cancellationToken);
        }

        _repository.Update(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok(employee.Id);
    }
}