
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Domain.Models.Categories;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler(IRepository<Category> categoryRepository) : ICommandHandler<DeleteCategoryCommand, Guid>
{
    public async Task<Response<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            return Response<Guid>.NotFound("Category not found.");
        }

        await categoryRepository.DeleteAsync(category, cancellationToken);
        return Response<Guid>.Success(request.Id);
    }
}