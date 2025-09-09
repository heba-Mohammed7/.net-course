namespace studentCourse.Features.Student.Query.Models;

public class GetStudentById : IRequest<Response>
{
    public int Id { get; set; }
}