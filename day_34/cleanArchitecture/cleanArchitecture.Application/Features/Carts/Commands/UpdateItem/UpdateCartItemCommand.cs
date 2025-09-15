using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Carts.Commands.UpdateItem;

public record UpdateCartItemCommand(Guid ItemId, int Quantity) : ICommand<string>;
