using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Features.Categories.Dtos;
using cleanArchitecture.Domain.Filters;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Categories.Queries.GetAll;

public record GetAllCategoriesQuery(string? Name): BaseFilter, IQuery<PaginatedResult<CategoryDto>>;