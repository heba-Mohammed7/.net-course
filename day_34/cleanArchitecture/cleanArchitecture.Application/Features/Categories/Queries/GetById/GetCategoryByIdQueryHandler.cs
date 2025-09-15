using AutoMapper;
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Categories.Dtos;
using cleanArchitecture.Domain.Models.Categories;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Categories.Queries.GetById;

public class GetCategoryByIdQueryHandler(IMapper mapper, IReadRepository<Category> CategoryRepository) : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<Response<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await CategoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
        {
            return Response<CategoryDto>.NotFound("category not found");
        }

        var categoryDto = mapper.Map<CategoryDto>(category);
        return Response<CategoryDto>.Success(categoryDto);
    }
}