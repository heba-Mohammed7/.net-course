
using AutoMapper;
using cleanArchitecture.Application.Features.Products.Commands.Add;
using cleanArchitecture.Application.Features.Products.Commands.Update;
using cleanArchitecture.Application.Features.Products.Dtos;

namespace cleanArchitecture.Application.Mapping.Product;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductCommand, Domain.Models.Products.Product>();
        CreateMap<UpdateProductCommand, Domain.Models.Products.Product>();
        CreateMap<Domain.Models.Products.Product, ProductDto>();
    }
}