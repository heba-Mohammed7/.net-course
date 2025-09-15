using AutoMapper;
using cleanArchitecture.Application.Features.Carts.Commands.AddItem;
using cleanArchitecture.Application.Features.Carts.Commands.UpdateItem;
using cleanArchitecture.Application.Features.Carts.Dtos;
using cleanArchitecture.Domain.Models.Carts;

namespace cleanArchitecture.Application.Mapping.Cart;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Domain.Models.Carts.Cart, CartDto>();
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

        CreateMap<AddToCartCommand, Domain.Models.Carts.Cart>();
        CreateMap<AddToCartCommand, CartItem>();
    }
}
