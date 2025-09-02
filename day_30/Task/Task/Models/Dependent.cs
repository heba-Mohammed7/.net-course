namespace Task.Models;

public class Dependent
{
    public int Id { get; set; }
    public string DName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}