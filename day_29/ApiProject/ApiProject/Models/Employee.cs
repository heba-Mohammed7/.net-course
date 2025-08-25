namespace ApiProject.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int LoginId { get; set; }
    public Login Login { get; set; }
}