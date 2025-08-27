using ApiProject.Models;

namespace ApiProject.Dto;

public class RoleResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<EmployeeResponseDto> Employees { get; set; } = new List<EmployeeResponseDto>();}