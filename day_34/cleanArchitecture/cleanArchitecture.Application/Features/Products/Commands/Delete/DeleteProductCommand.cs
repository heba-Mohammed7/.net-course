
using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : ICommand<Guid>;