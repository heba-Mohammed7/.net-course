using studentCourse.Models;
using studentCourse.Specifications.Base;
using studentCourse.Features.Course.Command.Models;

namespace studentCourse.Specifications;

public class CourseSpecification : BaseSpecification<CourseEntity>
{
    public CourseSpecification(CourseResponseDto courseDto = null) 
        : base()
    {
        if (courseDto != null)
        {
            if (!string.IsNullOrEmpty(courseDto.Cname))
            {
                AddCriteria(x => x.Cname.Contains(courseDto.Cname));
            }
            if (!string.IsNullOrEmpty(courseDto.Code))
            {
                AddCriteria(x => x.Code == courseDto.Code);
            }
            if (courseDto.Hours > 0)
            {
                AddCriteria(x => x.Hours == courseDto.Hours);
            }
        }

        AddInclude(x => x.Learns); 
        ApplyOrderByAsc(x => x.Cname);
    }
}