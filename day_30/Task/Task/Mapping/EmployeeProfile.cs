using AutoMapper;
using Task.Models;
using Task.Dto;
namespace Task.Mapping;

public class EmployeeProfile: Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Department, opt => opt.Ignore())
            .ForMember(dest => dest.Dependents, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeProjects, opt => opt.Ignore())
            .ForMember(dest => dest.ManagedDepartments, opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore()); 

        CreateMap<Employee, EmployeeResponseDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImagePath));

    }
}