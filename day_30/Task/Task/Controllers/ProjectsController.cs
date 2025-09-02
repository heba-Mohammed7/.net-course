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
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Project> _repository;

    public ProjectsController(ApplicationDbContext context, IMapper mapper, IGenericRepository<Project> repository)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(ProjectDto projectDto, CancellationToken cancellationToken)
    {
        var project = _mapper.Map<Project>(projectDto);
        _repository.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        var getDto = _mapper.Map<ProjectDto>(project);
        return Ok(project.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects(int pageNumber = 1, int pageSize = 10)
    {
        var query = _repository.GetQueryable()
            .Include(p => p.Department);
        var items = await query.OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var projectDtos = _mapper.Map<IEnumerable<ProjectResponseDto>>(items);
        return Ok(projectDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await _repository.GetQueryable()
            .Include(p => p.Department)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        var projectDto = _mapper.Map<ProjectResponseDto>(project);
        return Ok(projectDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _repository.Delete(project);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectResponseDto updatedProjectDto, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _mapper.Map(updatedProjectDto, project);

        _repository.Update(project);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok(project.Id);
    }
}