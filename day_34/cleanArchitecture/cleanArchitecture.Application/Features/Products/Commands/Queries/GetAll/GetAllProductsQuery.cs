using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Features.Products.Dtos;
using cleanArchitecture.Domain.Filters;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Products.Queries.GetAll;

public record GetAllProductsQuery(string? Name, decimal? MinPrice = null, decimal? MaxPrice = null) : ProductFilter(MinPrice: MinPrice, MaxPrice: MaxPrice), IQuery<PaginatedResult<ProductDto>>;