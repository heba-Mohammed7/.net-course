using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Products.Commands.Update;

public record UpdateProductCommand(Guid Id, string Name, decimal Price, Guid CategoryId) : ICommand<string>;
