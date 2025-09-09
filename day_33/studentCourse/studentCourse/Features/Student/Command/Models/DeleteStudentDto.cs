namespace studentCourse.Features.Student.Command.Models;

public class DeleteStudentDto : IRequest<Response>
{
    public int Id { get; set; }
}