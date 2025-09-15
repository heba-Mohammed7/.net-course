using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Products.Commands.Add;

public record AddProductCommand(string Name, decimal Price, Guid CategoryId) : ICommand<string>;