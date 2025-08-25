namespace ApiProject.Models;

public class Login
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}