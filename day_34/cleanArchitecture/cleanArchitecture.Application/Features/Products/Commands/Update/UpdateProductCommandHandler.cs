using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Domain.Models.Products;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Products.Commands.Update;

public class UpdateProductCommandHandler(IRepository<Product> productRepository) : ICommandHandler<UpdateProductCommand, string>
{
    public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return Response<string>.NotFound("Product not found.");
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;

        await productRepository.UpdateAsync(product, cancellationToken);
        return Response<string>.Success("Product updated successfully");
    }
}
