using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Features.Categories.Dtos;

namespace cleanArchitecture.Application.Features.Categories.Queries.GetById;

public record GetCategoryByIdQuery(Guid Id) : IQuery<CategoryDto>;