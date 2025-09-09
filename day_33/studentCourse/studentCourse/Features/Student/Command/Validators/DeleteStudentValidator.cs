namespace studentCourse.Features.Student.Command.Validators;

public class DeleteStudentValidator : AbstractValidator<DeleteStudentDto>
{
    public DeleteStudentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Student Id is required");
    }
}