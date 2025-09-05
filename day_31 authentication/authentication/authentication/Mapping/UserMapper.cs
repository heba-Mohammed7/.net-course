using authentication.Dtos;
using authentication.Models;
using AutoMapper;

namespace authentication.Mapping;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<RegisterModel, ApplicationUser>();
    }
}