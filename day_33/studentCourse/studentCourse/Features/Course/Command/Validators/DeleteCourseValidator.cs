namespace studentCourse.Features.Course.Command.Validators;

public class DeleteCourseValidator : AbstractValidator<DeleteCourseDto>
{
    public DeleteCourseValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Course Id is required");
    }
}