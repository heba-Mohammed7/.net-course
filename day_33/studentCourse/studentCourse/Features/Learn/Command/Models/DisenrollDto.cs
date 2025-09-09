namespace studentCourse.Features.Learn.Command.Models;
public class DisenrollDto : IRequest<Response>
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}