namespace studentCourse.Features.Student.Command.Models;

public class StudentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> CourseNames { get; set; }
}