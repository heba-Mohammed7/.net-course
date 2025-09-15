
using AutoMapper;
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Domain.Models.Categories;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Categories.Commands.Add;

public class AddCategoryCommandHandler(IMapper mapper, IRepository<Category> categoryRepository) : ICommandHandler<AddCategoryCommand, Guid>
{
    public async Task<Response<Guid>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request);
        await categoryRepository.AddAsync(category, cancellationToken);
        return  Response<Guid>.Created(category.Id);
    }
}