using ApiProject.Dto;
using ApiProject.Models;
using AutoMapper;

namespace ApiProject.Mapping;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentDto, Department>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore());

        CreateMap<Department, DepartmentResponseDto>();
    }
}