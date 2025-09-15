using AutoMapper;
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Products.Dtos;
using cleanArchitecture.Domain.Responses;
using cleanArchitecture.Application.Features.Products.Specifications;
using cleanArchitecture.Domain.Models.Products;

namespace cleanArchitecture.Application.Features.Products.Queries.GetAll;

public class GetAllProductsQueryHandler(IMapper mapper, IReadRepository<Product> productRepository) : IQueryHandler<GetAllProductsQuery, PaginatedResult<ProductDto>>
{
    public async Task<Response<PaginatedResult<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository
            .ListAsync(new ProductsSpec(request.Name, 
                request.MinPrice,
                request.MaxPrice,
                request.PageSize, 
                request.PageNumber), cancellationToken);

        var productsCount = await productRepository
            .CountAsync(new ProductsSpec(request.Name,
                request.MinPrice,
                request.MaxPrice,
                request.PageSize,
                request.PageNumber), cancellationToken);
        
        var mappedProducts = mapper.Map<IEnumerable<ProductDto>>(products);
        
        return Response<ProductDto>.GetData(mappedProducts ,request.PageNumber, request.PageSize, productsCount);
    }
}