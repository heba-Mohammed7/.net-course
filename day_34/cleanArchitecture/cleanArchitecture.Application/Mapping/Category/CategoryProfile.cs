
using cleanArchitecture.Application.Features.Categories.Commands.Add;
using cleanArchitecture.Application.Features.Categories.Commands.Update;
using cleanArchitecture.Application.Features.Categories.Dtos;

namespace cleanArchitecture.Application.Mapping.Category;

public class CategoryProfile : AutoMapper.Profile
{
    public CategoryProfile()
    {
        CreateMap<Domain.Models.Categories.Category, AddCategoryCommand>().ReverseMap();
        CreateMap<Domain.Models.Categories.Category, CategoryDto>();
        CreateMap<AddCategoryCommand, Domain.Models.Categories.Category>();
        CreateMap<UpdateCategoryCommand, Domain.Models.Categories.Category>();
    }
}