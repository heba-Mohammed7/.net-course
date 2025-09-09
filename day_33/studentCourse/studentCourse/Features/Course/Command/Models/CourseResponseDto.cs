namespace studentCourse.Features.Course.Command.Models;

public class CourseResponseDto
{
    public int Id { get; set; }
    public string Cname { get; set; }
    public string Code { get; set; }
    public int Hours { get; set; }
    public List<string> StudentNames { get; set; }
}