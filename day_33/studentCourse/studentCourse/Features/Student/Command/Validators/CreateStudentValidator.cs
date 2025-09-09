namespace studentCourse.Features.Student.Command.Validators;

public class CreateStudentValidator : AbstractValidator<StudentDto>
{
    public CreateStudentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Student name is required");
        RuleFor(x => x.Age)
            .GreaterThan(0)
            .WithMessage("Age must be greater than 0");
    }
}