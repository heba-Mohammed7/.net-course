
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Domain.Models.Products;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Products.Commands.Delete;

public class DeleteProductCommandHandler(IRepository<Product> productRepository) : ICommandHandler<DeleteProductCommand, Guid>
{
    public async Task<Response<Guid>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return Response<Guid>.NotFound("Product not found.");
        }

        await productRepository.DeleteAsync(product, cancellationToken);
        return Response<Guid>.Success(request.Id);
    }
}