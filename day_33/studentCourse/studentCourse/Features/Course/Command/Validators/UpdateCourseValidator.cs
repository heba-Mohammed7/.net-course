namespace studentCourse.Features.Course.Command.Validators;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
{
    public UpdateCourseValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Course Id is required");
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