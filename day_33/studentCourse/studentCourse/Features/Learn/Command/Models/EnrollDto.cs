namespace studentCourse.Features.Learn.Command.Models;

public class EnrollDto : IRequest<Response>
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}