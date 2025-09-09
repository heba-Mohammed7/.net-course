namespace studentCourse.Features.Student.Command.Validators;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Student Id is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Student name is required");
        RuleFor(x => x.Age)
            .GreaterThan(0)
            .WithMessage("Age must be greater than 0");
    }
}