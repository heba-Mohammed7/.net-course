using studentCourse.Models;
using studentCourse.Specifications.Base;
using studentCourse.Features.Student.Command.Models;

namespace studentCourse.Specifications;

public class StudentSpecification : BaseSpecification<StudentEntity>
{
    public StudentSpecification(StudentResponseDto studentDto = null)
    {
        if (studentDto != null)
        {
            if (!string.IsNullOrEmpty(studentDto.Name))
            {
                AddCriteria(x => x.Name.Contains(studentDto.Name));
            }
            if (studentDto.Age > 0)
            {
                AddCriteria(x => x.Age == studentDto.Age);
            }
        }

        AddInclude(x => x.Learns);
        ApplyOrderByAsc(x => x.Name);
    }
}