using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Features.Products.Dtos;

namespace cleanArchitecture.Application.Features.Products.Queries.GetById;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto>;