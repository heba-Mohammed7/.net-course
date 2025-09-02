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
public class EmployeeProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<EmployeeProject> _repository;

    public EmployeeProjectsController(ApplicationDbContext context, IMapper mapper, IGenericRepository<EmployeeProject> repository)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeProject(EmployeeProjectDto employeeProjectDto, CancellationToken cancellationToken)
    {
        var employeeProject = _mapper.Map<EmployeeProject>(employeeProjectDto);
        _repository.Add(employeeProject);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("cteated successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeeProjects(int pageNumber = 1, int pageSize = 10)
    {
        var query = _repository.GetQueryable()
            .Include(ep => ep.Employee)
            .Include(ep => ep.Project);
        var items = await query.OrderBy(ep => ep.EmployeeId)
            .ThenBy(ep => ep.ProjectId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var employeeProjectDtos = _mapper.Map<IEnumerable<EmployeeProjectResponseDto>>(items);
        return Ok(employeeProjectDtos);
    }
    
    
    [HttpDelete("{employeeId:int}/{projectId:int}")]
    public async Task<IActionResult> DeleteEmployeeProject(int employeeId, int projectId)
    {
        var employeeProject = await _repository.GetQueryable().FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);
        if (employeeProject == null)
        {
            return NotFound();
        }

        _repository.Delete(employeeProject);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{employeeId:int}/{projectId:int}")]
    public async Task<IActionResult> UpdateEmployeeProject(int employeeId, int projectId, EmployeeProjectDto updatedEmployeeProjectDto, CancellationToken cancellationToken)
    {
        var employeeProject = await _repository.GetQueryable().FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);
        if (employeeProject == null)
        {
            return NotFound();
        }

        _mapper.Map(updatedEmployeeProjectDto, employeeProject);

        _repository.Update(employeeProject);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok("updated successfully");
    }
}