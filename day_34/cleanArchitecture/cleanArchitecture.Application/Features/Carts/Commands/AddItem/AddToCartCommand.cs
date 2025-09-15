using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Carts.Commands.AddItem;

public record AddToCartCommand(string SessionId, Guid ProductId, int Quantity) : ICommand<string>;
