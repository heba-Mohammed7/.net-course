namespace studentCourse.Features.Course.Command.Models;

public class CourseDto : IRequest<Response>
{
    public string Cname { get; set; }
    public string Code { get; set; }
    public int Hours { get; set; }
    
}