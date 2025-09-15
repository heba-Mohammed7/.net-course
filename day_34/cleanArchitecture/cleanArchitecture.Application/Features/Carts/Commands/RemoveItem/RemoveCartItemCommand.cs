using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Carts.Commands.RemoveItem;

public record RemoveCartItemCommand(Guid ItemId) : ICommand<string>;
