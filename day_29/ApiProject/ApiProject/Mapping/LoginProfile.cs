using ApiProject.Dto;
using ApiProject.Models;
using AutoMapper;

namespace ApiProject.Mapping;

public class LoginProfile: Profile
{
    public LoginProfile()
    {
        CreateMap<LoginDto, Login>()
            .ForMember(dest => dest.Employee, opt => opt.Ignore());

        CreateMap<Login, LoginResponseDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty)); 
        
    }
}