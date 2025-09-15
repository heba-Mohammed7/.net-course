using AutoMapper;
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Domain.Models;
using cleanArchitecture.Domain.Models.Products;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Products.Commands.Add;

public class AddProductCommandHandler(IMapper mapper, IRepository<Product> productRepository) : ICommandHandler<AddProductCommand, string>
{
    public async Task<Response<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);
        await productRepository.AddAsync(product, cancellationToken);
        return Response<string>.Success("Product added successfully");
        
    }
}