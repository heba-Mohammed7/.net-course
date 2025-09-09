namespace studentCourse.Mapper;

public class CourseMapper : Profile
{
    public CourseMapper()
    {
        CreateMap<CourseDto, CourseEntity>();

        CreateMap<UpdateCourseDto, CourseEntity>();
        
        CreateMap<CourseEntity, CourseResponseDto>()
            .ForMember(dest => dest.StudentNames, 
                opt => opt.MapFrom(src => src.Learns.Select(l => l.Student.Name).ToList()));
    }
}