using AutoMapper;
using Task.Dto;
using Task.Models;

namespace Task.Mapping;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentDto, Department>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore())
            .ForMember(dest => dest.Projects, opt => opt.Ignore())
            .ForMember(dest => dest.Manager, opt => opt.Ignore());

        CreateMap<Department, DepartmentResponseDto>()
            .ForMember(dest => dest.ManagerName,
                opt => opt.MapFrom(src => src.Manager != null ? src.Manager.Name : null));
    }
}