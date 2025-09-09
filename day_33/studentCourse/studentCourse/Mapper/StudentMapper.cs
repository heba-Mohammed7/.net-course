namespace studentCourse.Mapper;

public class StudentMapper : Profile
{
    public StudentMapper()
    {
        CreateMap<StudentDto, StudentEntity>();

        CreateMap<UpdateStudentDto, StudentEntity>();
        CreateMap<StudentEntity, StudentResponseDto>()
            .ForMember(dest => dest.CourseNames, 
                opt => opt.MapFrom(src => src.Learns.Select(l => l.Course.Cname).ToList()));
    }
}