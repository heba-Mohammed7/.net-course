using ApiProject.Data;
using ApiProject.Dto;
using ApiProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Controller;
[ApiController]
[Route("api/[controller]")]
public class EmployeeController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(employeeDto);
        await context.Employees.AddAsync(employee, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Ok(employee.Id);
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployeeById(int id)
    {
        var employee = context.Employees
            .Include(e => e.Role)
            .Include(e => e.Department)
            .Include(e => e.Login)
            .FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        var employeeDto = mapper.Map<EmployeeResponseDto>(employee);
        return Ok(employeeDto);
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
        var employees = context.Employees.ToList();
        var employeeDtos = mapper.Map<List<EmployeeResponseDto>>(employees);
        return Ok(employeeDtos);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = context.Employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        
        context.Employees.Remove(employee);
        context.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateEmployee(int id, EmployeeDto updatedEmployee)
    {
        var employee = context.Employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        
        var newEmployee = mapper.Map(updatedEmployee, employee);
        context.Employees.Update(newEmployee);
        context.SaveChanges();
        
        return Ok("updated successfully");
    }
}