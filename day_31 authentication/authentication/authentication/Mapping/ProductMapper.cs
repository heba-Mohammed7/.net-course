using authentication.Dtos;
using authentication.Models;
using authentication.Services.Implementations;
using AutoMapper;

namespace authentication.Mapping;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
    }
}