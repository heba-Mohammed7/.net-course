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
public class DependentsController(ApplicationDbContext _context, IMapper _mapper, IGenericRepository<Dependent> _repository) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateDependent(DependentDto dependentDto, CancellationToken cancellationToken)
    {
        var dependent = _mapper.Map<Dependent>(dependentDto);
        _repository.Add(dependent);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok(dependent.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetDependents(int pageNumber = 1, int pageSize = 10)
    {
        var query = _repository.GetQueryable()
            .Include(d => d.Employee);
        var items = await query.OrderBy(d => d.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var dependentDtos = _mapper.Map<IEnumerable<DependentResponseDto>>(items);
        return Ok(dependentDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDependentById(int id)
    {
        var dependent = await _repository.GetQueryable()
            .Include(d => d.Employee)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (dependent == null)
        {
            return NotFound();
        }
        var dependentDto = _mapper.Map<DependentResponseDto>(dependent);
        return Ok(dependentDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDependent(int id)
    {
        var dependent = await _repository.GetByIdAsync(id);
        if (dependent == null)
        {
            return NotFound();
        }

        _repository.Delete(dependent);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDependent(int id, DependentDto updatedDependentDto, CancellationToken cancellationToken)
    {
        var dependent = await _repository.GetByIdAsync(id);
        if (dependent == null)
        {
            return NotFound();
        }

        _mapper.Map(updatedDependentDto, dependent);

        _repository.Update(dependent);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok(dependent.Id);
    }
}