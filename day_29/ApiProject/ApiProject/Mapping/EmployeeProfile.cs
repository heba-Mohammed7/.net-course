using ApiProject.Dto;
using ApiProject.Models;
using AutoMapper;

namespace ApiProject.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Department, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.Login, opt => opt.Ignore());

        CreateMap<Employee, EmployeeResponseDto>()
            .ForMember(dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : string.Empty));
    }
}