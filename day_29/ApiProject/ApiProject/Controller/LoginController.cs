using ApiProject.Data;
using ApiProject.Dto;
using ApiProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Controller;
[ApiController]
[Route("api/[controller]")]
public class LoginController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateLogin([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var employee = await context.Employees.FindAsync(new object[] { loginDto.EmployeeId }, cancellationToken);
        if (employee == null)
        {
            return BadRequest("Invalid EmployeeId. The employee does not exist.");
        }

        var existingLogin = await context.Logins
            .FirstOrDefaultAsync(l => l.EmployeeId == loginDto.EmployeeId, cancellationToken);
        if (existingLogin != null)
        {
            return BadRequest("A login already exists for this employee.");
        }

        var login = mapper.Map<Login>(loginDto);
        login.Employee = employee;
        await context.Logins.AddAsync(login, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var loginResponseDto = mapper.Map<LoginResponseDto>(login);
        return CreatedAtAction(nameof(GetLoginById), new { id = login.Id }, loginResponseDto);
    }    
    
    [HttpGet("{id}")]
    public IActionResult GetLoginById(int id)
    {
        var login = context.Logins
            .Include(l => l.Employee)
            .FirstOrDefault(l => l.Id == id);
        if (login == null)
        {
            return NotFound();
        }

        var loginDto = mapper.Map<LoginResponseDto>(login);
        return Ok(loginDto);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteLogin(int id)
    {
        var login = context.Logins.FirstOrDefault(l => l.Id == id);
        if (login == null)
        {
            return NotFound();
        }
        
        context.Logins.Remove(login);
        context.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateLogin(int id, LoginDto updatedLogin)
    {
        var login = context.Logins
            .Include(l => l.Employee)
            .FirstOrDefault(l => l.Id == id);
        if (login == null)
        {
            return NotFound();
        }

        var newLogin = mapper.Map(updatedLogin, login);
        context.Logins.Update(newLogin);
        context.SaveChanges();

        var loginResponseDto = mapper.Map<LoginResponseDto>(login);
        return Ok(loginResponseDto);
    }
}