using AutoMapper;
using Task.Dto;
using Task.Models;

namespace Task.Mapping;

public class ProjectProfile:Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectDto, Project>()
            .ForMember(dest => dest.Department, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeProjects, opt => opt.Ignore());

        CreateMap<Project, ProjectResponseDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

    }
}