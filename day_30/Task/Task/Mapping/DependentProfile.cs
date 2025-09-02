using AutoMapper;
using Task.Dto;
using Task.Models;

namespace Task.Mapping;

public class DependentProfile : Profile
{
    public DependentProfile()
    {
        CreateMap<DependentDto, Dependent>()
            .ForMember(dest => dest.Employee, opt => opt.Ignore());

        CreateMap<Dependent, DependentResponseDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty));
    }
}