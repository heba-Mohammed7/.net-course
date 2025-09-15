using AutoMapper;
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Products.Dtos;
using cleanArchitecture.Domain.Models.Products;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Products.Queries.GetById;

public class GetProductByIdQueryHandler(IMapper mapper, IReadRepository<Product> productRepository) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<Response<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
        {
            return Response<ProductDto>.NotFound("Products not found");
        }

        var productDto = mapper.Map<ProductDto>(product);
        return Response<ProductDto>.Success(productDto);
    }
}