namespace studentCourse.Features.Learn.Command.Validators;

public class EnrollValidator : AbstractValidator<EnrollDto>
{
    public EnrollValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(0);
        RuleFor(x => x.CourseId).GreaterThan(0);
    }
}