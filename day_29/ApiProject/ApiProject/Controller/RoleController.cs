using ApiProject.Data;
using ApiProject.Dto;
using ApiProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Controller;
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public RoleController(ApplicationDbContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleDto roleDto, CancellationToken cancellationToken)
    {
        var role = mapper.Map<Role>(roleDto);
        await context.Roles.AddAsync(role, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
    }

    [HttpGet("{id}")]
    public IActionResult GetRoleById(int id)
    {
        var role = context.Roles
            .Include(r => r.Employees)
            .ThenInclude(e => e.Department) 
            .FirstOrDefault(r => r.Id == id);
        if (role == null)
        {
            return NotFound();
        }
        var roleDto = mapper.Map<RoleResponseDto>(role);
        return Ok(roleDto);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRole(int id)
    {
        var role = context.Roles.FirstOrDefault(r => r.Id == id);
        if (role == null)
        {
            return NotFound();
        }
        
        context.Roles.Remove(role);
        context.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateRole(int id, RoleDto updatedRole)
    {
        var role = context.Roles.FirstOrDefault(r => r.Id == id);
        if (role == null)
        {
            return NotFound();
        }
        
        var newRole = mapper.Map(updatedRole, role);
        context.Roles.Update(newRole);
        context.SaveChanges();
        
        return Ok(role);
    }
}