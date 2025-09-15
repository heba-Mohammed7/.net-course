using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Domain.Models.Categories;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Categories.Commands.Update;

public class UpdateCategoryCommandHandler(IRepository<Category> categoryRepository) : ICommandHandler<UpdateCategoryCommand, string>
{
    public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            return Response<string>.NotFound("Category not found.");
        }

        category.Name = request.Name;
        category.UpdatedAt = DateTime.UtcNow;

        await categoryRepository.UpdateAsync(category, cancellationToken);
        return Response<string>.Success("Category updated successfully");
    }
}
