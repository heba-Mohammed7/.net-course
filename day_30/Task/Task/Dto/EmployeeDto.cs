namespace Task.Dto;

public class EmployeeDto
{
    public string Name { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public DateTime Doj { get; set; }
    public int? DepartmentId { get; set; }
    public DateTime WorksInSince { get; set; }
    public IFormFile? Image { get; set; }
}
