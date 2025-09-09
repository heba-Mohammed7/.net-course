namespace studentCourse.Features.Course.Query.Models;

public class GetCourseById : IRequest<Response>
{
    public int Id { get; set; }
}