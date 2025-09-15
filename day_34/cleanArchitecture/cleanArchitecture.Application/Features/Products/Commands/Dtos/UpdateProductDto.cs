namespace cleanArchitecture.Application.Features.Products.Commands.Update;

public record UpdateProductDto(string Name, decimal Price, Guid CategoryId);
