namespace studentCourse.Features.Course.Query.Models;
public class GetAllCoursesDto : IRequest<Response>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}