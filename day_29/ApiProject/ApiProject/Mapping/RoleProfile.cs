using ApiProject.Dto;
using ApiProject.Models;
using AutoMapper;

namespace ApiProject.Mapping;

public class RoleProfile: Profile
{
    public RoleProfile()
    {
        CreateMap<RoleDto, Role>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore());

        CreateMap<Role, RoleResponseDto>()
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
        
    }
    
}
