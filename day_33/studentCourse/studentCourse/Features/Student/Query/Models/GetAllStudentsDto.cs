namespace studentCourse.Features.Student.Query.Models;

public class GetAllStudentsDto : IRequest<Response>
{
    public string? Name { get; set; }
    public int? Age { get; set; }
}