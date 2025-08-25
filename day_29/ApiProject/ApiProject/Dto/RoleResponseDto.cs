using ApiProject.Models;

namespace ApiProject.Dto;

public class RoleResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<EmployeeResponseDto> Employees { get; set; } = new List<EmployeeResponseDto>();}