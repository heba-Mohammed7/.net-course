namespace studentCourse.Features.Student.Command.Models;

public class StudentDto : IRequest<Response>
{
    public string Name { get; set; }
    public int Age { get; set; }
    
}