using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Carts.Specifications;
using cleanArchitecture.Domain.Models.Carts;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Carts.Commands.RemoveItem;

public class RemoveCartItemCommandHandler(
    IRepository<Cart> cartRepository,
    IRepository<CartItem> cartItemRepository) : ICommandHandler<RemoveCartItemCommand, string>
{
    public async Task<Response<string>> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.ItemId, cancellationToken);
        if (cartItem is null)
        {
            return Response<string>.NotFound("Cart item not found.");
        }

        var cartId = cartItem.CartId;
        await cartItemRepository.DeleteAsync(cartItem, cancellationToken);

        var cart = await cartRepository.GetByIdAsync(cartId, cancellationToken);
        if (cart != null)
        {
            var cartItems = await cartItemRepository.ListAsync(new CartItemsByCartIdSpec(cart.Id), cancellationToken);
            cart.TotalAmount = cartItems.Sum(ci => ci.TotalPrice);
            cart.ItemCount = cartItems.Sum(ci => ci.Quantity);
            cart.UpdatedAt = DateTime.UtcNow;
            await cartRepository.UpdateAsync(cart, cancellationToken);
        }

        return Response<string>.Success("Item removed from cart successfully");
    }
}
