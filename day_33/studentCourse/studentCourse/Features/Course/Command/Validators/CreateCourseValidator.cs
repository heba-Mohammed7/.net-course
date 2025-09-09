namespace studentCourse.Features.Course.Command.Validators;

public class CreateCourseValidator : AbstractValidator<CourseDto>
{
    public CreateCourseValidator()
    {
        RuleFor(x => x.Cname)
            .NotEmpty()
            .WithMessage("Course name is required");
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Course code is required");
        RuleFor(x => x.Hours)
            .GreaterThan(0)
            .WithMessage("Hours must be greater than 0");
    }
}