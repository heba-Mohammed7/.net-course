namespace Task.Dto;

public class DepartmentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; } = string.Empty;
    public int? ManagerId { get; set; }
    public string? ManagerName { get; set; }
    public ICollection<EmployeeResponseDto> Employees { get; set; } = new List<EmployeeResponseDto>();
    
}