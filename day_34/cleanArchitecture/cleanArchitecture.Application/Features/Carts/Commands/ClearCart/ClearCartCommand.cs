using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Carts.Commands.ClearCart;

public record ClearCartCommand(string SessionId) : ICommand<string>;
