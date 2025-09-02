using AutoMapper;
using Task.Dto;
using Task.Models;

namespace Task.Mapping;

public class EmployeeProjectProfile : Profile
{
    public EmployeeProjectProfile()
    {
        CreateMap<EmployeeProjectDto, EmployeeProject>()
            .ForMember(dest => dest.Employee, opt => opt.Ignore())
            .ForMember(dest => dest.Project, opt => opt.Ignore());

        CreateMap<EmployeeProject, EmployeeProjectResponseDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : string.Empty));
    }
}