namespace Task.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public DateTime Doj { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public int? DepartmentId { get; set; }  // Made nullable to break circular dependency
    public Department? Department { get; set; }
    public DateTime WorksInSince { get; set; }
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
    public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    public ICollection<Department> ManagedDepartments { get; set; } = new List<Department>();

}