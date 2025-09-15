using AutoMapper;
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Categories.Dtos;
using cleanArchitecture.Application.Features.Categories.Specifications;
using cleanArchitecture.Application.Features.Products.Dtos;
using cleanArchitecture.Domain.Responses;
using cleanArchitecture.Domain.Models.Categories;
using cleanArchitecture.Domain.Models.Products;

namespace cleanArchitecture.Application.Features.Categories.Queries.GetAll;

public class GetAllCategoriesQueryHandler(IMapper mapper, IReadRepository<Category> CategoryRepository) : IQueryHandler<GetAllCategoriesQuery, PaginatedResult<CategoryDto>>
{
    public async Task<Response<PaginatedResult<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await CategoryRepository
            .ListAsync(new CategoriesSpec(request.Name, 
                request.PageSize, 
                request.PageNumber), cancellationToken);

        var categoriesCount = await CategoryRepository
            .CountAsync(new CategoriesSpec(request.Name,
                request.PageSize,
                request.PageNumber), cancellationToken);
        
        var mappedProducts = mapper.Map<IEnumerable<CategoryDto>>(categories);
        
        return Response<CategoryDto>.GetData(mappedProducts ,request.PageNumber, request.PageSize, categoriesCount);
    }
}